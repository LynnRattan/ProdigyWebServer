using Newtonsoft.Json.Linq;
using ProdigyServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
                var response = await client.GetAsync($"{URL}?author={authorName}&limit=10");
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    
                    var content = JObject.Parse(await response.Content.ReadAsStringAsync());

                    List<PenguinResult> authors = new List<PenguinResult>();
                    var arr = content["docs"].ToArray();
                    if (arr.Length >= 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            authors.Add(new PenguinResult(
                                arr[i]["publisher"][0].ToString(),
                                arr[i]["author_key"][0].ToString(),
                                arr[i]["author_name"][0].ToString(),
                                arr[i]["title"].ToString()));
                        }
                    }

                    return authors;
                }
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<PenguinResult>();
                }
            }
            catch (Exception ex) { return null; };
            return null;
        }
    }
}
