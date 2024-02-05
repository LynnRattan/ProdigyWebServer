using Newtonsoft.Json.Linq;
using ProdigyServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//API: 
//https://openlibrary.org/dev/docs/api/books


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

        public async Task<List<PenguinResult>> GetBookByAuthor(string authorName)
        {
            try
            {
                var response = await client.GetAsync(URL + "?author="+authorName);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = JObject.Parse(await response.Content.ReadAsStringAsync());
                    List<PenguinResult> authors = new List<PenguinResult>();
                    for (int i = 0; i < 10; i++)
                    {
                        authors.Add(new PenguinResult(
                            content["docs"][i]["publisher"][0].ToString(), 
                            content["docs"][i]["author_key"][0].ToString(), 
                            content["docs"][i]["author_name"][0].ToString(), 
                            content["docs"][i]["title"].ToString()));
                    }
                    return authors;
                }
            }
            catch (Exception ex) { return null; };
            return null;
        }
    }
}
