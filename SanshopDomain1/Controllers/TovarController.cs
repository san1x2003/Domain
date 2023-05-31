using SanshopDomain1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Domain.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Domain/Tovar")]
    public class TovarController : ControllerBase
    {
        private readonly string? _dalUrl;
        private readonly HttpClient _client;

        public TovarController(IConfiguration conf)
        {
            _dalUrl = conf.GetValue<string>("DalUrl");
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<ActionResult<Tovar[]>> GetTovars()
        {
            var response = await _client.GetAsync($"{_dalUrl}/api/Tovar");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content == null) return NotFound();

            return JsonSerializer.Deserialize<Tovar[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<Tovar>();
        }
        [HttpPost]
        public async Task<ActionResult<Tovar>> PostTovar(Tovar tovar)
        {
            JsonContent content = JsonContent.Create(tovar);
            using var result = await _client.PostAsync($"{_dalUrl}/Tovar/PostTovar", content);
            var dalTovar = await result.Content.ReadFromJsonAsync<Tovar>();
            Console.WriteLine($"{dalTovar?.Id}");
            if (dalTovar == null)
                return BadRequest();
            else
                return dalTovar;
        }

    }
}