using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProdigyServerBL.Services
{
    public class PenguinServices
    {
        private HttpClient client;
        const string URL = @"https://openlibrary.org/search.json";

        public PenguinServices()
        {
            client = new HttpClient();
                
        }

        public async Task<string> GetBitches()
        {
            try
            {
                var response = await client.GetAsync(URL + "?q=the+lord+of+the+rings");
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.Out.WriteLine(await response.Content.ReadAsStringAsync());
                    return "kudasai";
                }
            }
            catch (Exception ex) { return ""; };
            return "";
        }
    }
}
