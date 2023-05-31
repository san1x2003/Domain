using SanshopDomain1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Domain.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Domain/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly string? _dalUrl;
        private readonly HttpClient _client;

        public EmployeeController(IConfiguration conf)
        {
            _dalUrl = conf.GetValue<string>("DalUrl");
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<ActionResult<Employee[]>> GetEmployees()
        {
            var response = await _client.GetAsync($"{_dalUrl}/api/Employee");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content == null) return NotFound();

            return JsonSerializer.Deserialize<Employee[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<Employee>();
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            JsonContent content = JsonContent.Create(employee);
            using var result = await _client.PostAsync($"{_dalUrl}/Employee/PostEmployee", content);
            var dalEmployee = await result.Content.ReadFromJsonAsync<Employee>();
            Console.WriteLine($"{dalEmployee?.Id}");
            if (dalEmployee == null)
                return BadRequest();
            else
                return dalEmployee;
        }

    }
}