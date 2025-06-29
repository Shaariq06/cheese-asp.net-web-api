using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebApplication.Models.DTO;

namespace MVCWebApplication.Controllers
{
    public class CheeseController : Controller
    {
        private readonly IHttpClientFactory _client;
        public CheeseController(IHttpClientFactory client)
        {
            _client = client;
        }
        // GET: Cheese
        public async Task<IActionResult> Index()
        {
            List<CheeseDTO> responseBody = new List<CheeseDTO>();
            try
            {
                var client = _client.CreateClient("IgnoreSSL");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0dXNlcjFAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTc1MTIyNDE4MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA1OSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwNTkifQ.UR4ORfCEKqniJeizwRtYj_zJnUwpPUjLsIo9_dlFLj8");

                var httpResponse = await client.GetAsync("https://localhost:7059/api/Cheese");

                httpResponse.EnsureSuccessStatusCode();

                var cheeses = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<CheeseDTO>>();
                responseBody.AddRange(cheeses ?? Enumerable.Empty<CheeseDTO>());
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }


            return View(responseBody);
        }
    }
}