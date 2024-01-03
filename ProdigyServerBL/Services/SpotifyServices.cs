using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Swan;

namespace ProdigyServerBL.Services
{
    public class SpotifyServices
    {
        private static EmbedIOAuthServer _server;

        public static async Task Main()
        {
            _server = new EmbedIOAuthServer(new Uri("http://localhost:5543/callback"), 5543);
            await _server.Start();


            _server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;
            _server.ErrorReceived += OnErrorReceived;

            var request = new LoginRequest(_server.BaseUri, "d3702b9c8fbf43c2a0249420c700a876", LoginRequest.ResponseType.Code)
            {
                Scope = new List<string> { Scopes.UserReadCurrentlyPlaying }
            };
            BrowserUtil.Open(request.ToUri());
            Task.Delay(3000).Wait();
        }

        private static async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            await _server.Stop();

            var config = SpotifyClientConfig.CreateDefault();
            var tokenResponse = await new OAuthClient(config).RequestToken(
              new AuthorizationCodeTokenRequest(
                "d3702b9c8fbf43c2a0249420c700a876", "3c3d755a7f184a4ab1dadbe38a9f2aad", response.Code, new Uri("http://localhost:5543/callback")
              )
            );

            var spotify = new SpotifyClient(tokenResponse.AccessToken);
            // do calls with Spotify and save token?
            var track = await spotify.Tracks.Get("1s6ux0lNiTziSrd7iUAADH");
            var searchResult = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Album, "fornite"));
            Console.WriteLine(searchResult);
            //var curr = await spotify.Player.GetCurrentlyPlaying(new PlayerCurrentlyPlayingRequest());
            //Console.WriteLine(curr.Item.ReadProperty("Name"));
        }

        private static async Task OnErrorReceived(object sender, string error, string state)
        {
            Console.WriteLine($"Aborting authorization, error received: {error}");
            await _server.Stop();
        }
    }
}

