using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Game_On.Models
{
    internal class apiRecuperateur
    {
        public async Task<ActionResult<string>> GetApiData(int level)
        {
            using HttpClient client = new HttpClient();
            string url = "https://youdosudoku.com/api/";
            string lvl;
            switch (level)
            {
                case 1:
                    lvl = "easy";
                    break;
                case 2:
                    lvl = "medium";
                    break;
                case 3:
                    lvl = "hard";
                    break;
                default:
                    lvl = "medium";
                    break;
            }

            var postData = new { difficulty = lvl, solution = true, array = false };
            string jsonContent = JsonConvert.SerializeObject(postData);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string responseBody = "";
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseBody}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request Error: {e.Message}");
            }

            // Simulate API data retrieval
            return responseBody;
        }
    }
}
