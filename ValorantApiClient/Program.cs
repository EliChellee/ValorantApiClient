using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ValorantApiClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            string apiUrl = "http://localhost/valorantapi/api.php";

            // Perform GET request
            await GetPlayers(apiUrl);

            // Perform POST request
            await CreatePlayer(apiUrl, "JaneDoe", "jane@example.com", "2000-01-01", "Gold", "password123", "active", 0, 4);
        }

        static async Task GetPlayers(string url)
        {
            var response = await client.GetStringAsync(url);
            var formattedResponse = JArray.Parse(response).ToString(Formatting.Indented);
            Console.WriteLine("GET Response:");
            Console.WriteLine(formattedResponse);
        }

        static async Task CreatePlayer(string url, string username, string email, string birthday, string player_rank, string password, string status, int is_admin, int player_rank_numeric)
        {
            var player = new
            {
                username,
                email,
                birthday,
                player_rank,
                password,
                status,
                is_admin,
                player_rank_numeric
            };
            var json = JsonConvert.SerializeObject(player);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);
            string result = await response.Content.ReadAsStringAsync();
            var formattedResult = JObject.Parse(result).ToString(Formatting.Indented);
            Console.WriteLine("POST Response:");
            Console.WriteLine(formattedResult);
        }
    }
}
