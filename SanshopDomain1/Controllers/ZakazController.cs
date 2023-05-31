using SanshopDomain1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Domain.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Domain/Zakaz")]
    public class ZakazController : ControllerBase
    {
        private readonly string? _dalUrl;
        private readonly HttpClient _client;

        public ZakazController(IConfiguration conf)
        {
            _dalUrl = conf.GetValue<string>("DalUrl");
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<ActionResult<Zakaz[]>> GetZakazs()
        {
            var response = await _client.GetAsync($"{_dalUrl}/api/Zakaz");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content == null) return NotFound();

            return JsonSerializer.Deserialize<Zakaz[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<Zakaz>();
        }
        [HttpPost]
        public async Task<ActionResult<Zakaz>> PostZakaz(Zakaz zakaz)
        {
            JsonContent content = JsonContent.Create(zakaz);
            using var result = await _client.PostAsync($"{_dalUrl}/Zakaz/PostZakaz", content);
            var dalZakaz = await result.Content.ReadFromJsonAsync<Zakaz>();
            Console.WriteLine($"{dalZakaz?.Id}");
            if (dalZakaz == null)
                return BadRequest();
            else
                return dalZakaz;
        }

    }
}