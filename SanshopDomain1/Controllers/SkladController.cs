using SanshopDomain1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Domain.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Domain/Sklad")]
    public class SkladController : ControllerBase
    {
        private readonly string? _dalUrl;
        private readonly HttpClient _client;

        public SkladController(IConfiguration conf)
        {
            _dalUrl = conf.GetValue<string>("DalUrl");
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<ActionResult<Sklad[]>> GetSklads()
        {
            var response = await _client.GetAsync($"{_dalUrl}/api/Sklad");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content == null) return NotFound();

            return JsonSerializer.Deserialize<Sklad[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<Sklad>();
        }
        [HttpPost]
        public async Task<ActionResult<Sklad>> PostSklad(Sklad sklad)
        {
            JsonContent content = JsonContent.Create(sklad);
            using var result = await _client.PostAsync($"{_dalUrl}/Sklad/PostSklad", content);
            var dalSklad = await result.Content.ReadFromJsonAsync<Sklad>();
            Console.WriteLine($"{dalSklad?.Id}");
            if (dalSklad == null)
                return BadRequest();
            else
                return dalSklad;
        }

    }
}