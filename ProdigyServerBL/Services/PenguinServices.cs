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
                    for (int i = 0; i < arr.Length; i++)
                    {
                        //PenguinResult resultToAdd = new PenguinResult();

                        //if (arr[i]["publisher"] != null ) { resultToAdd.Publisher = arr[i]["publisher"][0].ToString(); }
                        //else { resultToAdd.Publisher = "no information available"; };

                        //if (arr[i]["author_key"] != null) { resultToAdd.AuthorKey = arr[i]["author_key"][0].ToString(); }
                        //else { resultToAdd.AuthorKey = "no information available"; };

                        //if (arr[i]["author_name"] != null) { resultToAdd.AuthorName = arr[i]["author_name"][0].ToString(); }
                        //else { resultToAdd.AuthorName = "no information available"; };

                        //if (arr[i]["author_name"] != null) { resultToAdd.Title = arr[i]["title"][0].ToString(); }
                        //else { resultToAdd.Title = "no information available"; };

                        //if (arr[i]["isbn"] != null) { resultToAdd.ISBN = arr[i]["isbn"][0].ToString(); }
                        //else { resultToAdd.ISBN = "no information available"; };

                        
                        authors.Add(new PenguinResult(
                            arr[i]["publisher"][0].ToString(),
                            arr[i]["author_key"][0].ToString(),
                            arr[i]["author_name"][0].ToString(),
                            arr[i]["title"].ToString(),
                            arr[i]["isbn"][0].ToString()));
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
