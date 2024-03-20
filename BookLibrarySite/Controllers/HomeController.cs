using BookLibrarySite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace BookLibrarySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index(string searchField, string searchValue)
        {
            List<Book> books;
            if (!string.IsNullOrEmpty(searchField) && !string.IsNullOrEmpty(searchValue))
            {
                switch (searchField.ToLower())
                {
                    case "isbn":
                        var isbnResponse = await _httpClient.GetAsync($"http://localhost:5062/api/Book/isbn/{searchValue}");
                        if (isbnResponse.IsSuccessStatusCode)
                        {
                            var apiDataJson = await isbnResponse.Content.ReadAsStringAsync();
                            var apiData = JsonConvert.DeserializeObject<List<Book>>(apiDataJson);
                            return View(apiData);
                        }
                        else
                        {
                            // Handle error
                            return View("Error");
                        }
                    case "author":
                        var authorResponse = await _httpClient.GetAsync($"http://localhost:5062/api/Book/author/{searchValue}");
                        if (authorResponse.IsSuccessStatusCode)
                        {
                            var apiDataJson = await authorResponse.Content.ReadAsStringAsync();
                            var apiData = JsonConvert.DeserializeObject<List<Book>>(apiDataJson);
                            return View(apiData);
                        }
                        else
                        {
                            // Handle error
                            return View("Error");
                        }
                    // Add cases for other search fields if needed
                    default:
                        var response = await _httpClient.GetAsync("http://localhost:5062/api/Book");
                        if (response.IsSuccessStatusCode)
                        {
                            var apiDataJson = await response.Content.ReadAsStringAsync();
                            var apiData = JsonConvert.DeserializeObject<List<Book>>(apiDataJson);
                            return View(apiData);
                        }
                        else
                        {
                            // Handle error
                            return View("Error");
                        }
                }
            }
            else
            {
                var response = await _httpClient.GetAsync("http://localhost:5062/api/Book");
                if (response.IsSuccessStatusCode)
                {
                    var apiDataJson = await response.Content.ReadAsStringAsync();
                    var apiData = JsonConvert.DeserializeObject<List<Book>>(apiDataJson);
                    return View(apiData);
                }
                else
                {
                    // Handle error
                    return View("Error");
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
