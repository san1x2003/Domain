using SanshopDomain1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;



namespace Domain.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Domain/Post")]
    public class PostController : ControllerBase
    {
        private readonly string? _dalUrl;
        private readonly HttpClient _client;

        public PostController(IConfiguration conf)
        {
            _dalUrl = conf.GetValue<string>("DalUrl");
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<ActionResult<Post[]>> GetPosts()
        {
            var response = await _client.GetAsync($"{_dalUrl}/api/Post");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (content == null) return NotFound();

            return JsonSerializer.Deserialize<Post[]>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<Post>();
        }
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            JsonContent content = JsonContent.Create(post);
            using var result = await _client.PostAsync($"{_dalUrl}/Client/PostClient", content);
            var dalPost = await result.Content.ReadFromJsonAsync<Post>();
            Console.WriteLine($"{dalPost?.Id}");
            if (dalPost == null)
                return BadRequest();
            else
                return dalPost;
        }

    }
}