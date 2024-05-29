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

            // Perform GET request for players
            await GetPlayers(apiUrl + "?request=get_players");

            // Perform POST request to create a player
            await CreatePlayer(apiUrl + "?request=add_player", "JaneDoe", "jane@example.com", "2000-01-01", "Gold", "password123", "active", 0, 4);
        }

        static async Task GetPlayers(string url)
        {
            var response = await client.GetStringAsync(url);
            Console.WriteLine("Raw GET Players Response:");
            Console.WriteLine(response);
            try
            {
                var formattedResponse = JArray.Parse(response).ToString(Formatting.Indented);
                Console.WriteLine("Formatted GET Players Response:");
                Console.WriteLine(formattedResponse);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("Error parsing JSON response:");
                Console.WriteLine(e.Message);
            }
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
            Console.WriteLine("Raw POST Player Response:");
            Console.WriteLine(result);
            try
            {
                var formattedResult = JObject.Parse(result).ToString(Formatting.Indented);
                Console.WriteLine("Formatted POST Player Response:");
                Console.WriteLine(formattedResult);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("Error parsing JSON response:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
