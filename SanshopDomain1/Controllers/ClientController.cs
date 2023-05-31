using SanshopDomain1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Domain.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Domain/Client")]
    public class ClientController : ControllerBase
    {
        private readonly string? _dalUrl;
        private readonly HttpClient _client;

        public ClientController(IConfiguration conf)
        {
            _dalUrl = conf.GetValue<string>("DalUrl");
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<ActionResult<Client[]>> GetClients()
        {
            var response = await _client.GetAsync($"{_dalUrl}/api/Client");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content == null) return NotFound();

            return JsonSerializer.Deserialize<Client[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<Client>();
        }
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            JsonContent content = JsonContent.Create(client);
            using var result = await _client.PostAsync($"{_dalUrl}/api/Client/PostClient", content);
            var dalClient = await result.Content.ReadFromJsonAsync<Client>();
            Console.WriteLine($"{dalClient?.ClientId}");
            if (dalClient == null)
                return BadRequest();
            else
                return dalClient;
        }
        [HttpDelete("{id}")]
        public async Task DeleteClient(int id)
        {
            var response = await _client.DeleteAsync($"{_dalUrl}/api/Client/DeleteClient/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}