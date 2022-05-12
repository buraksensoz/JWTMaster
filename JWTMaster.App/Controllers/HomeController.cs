
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JWTMaster.App.Controllers
{
    public class HomeController : Controller
    {
        private static string generatedToken="";
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            
        }

        public IActionResult Index()
        {
            ViewBag.Token=generatedToken;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {

            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("WebServiceAddress"));
            

            var responseMessage = await _httpClient.GetAsync("member/SignIn");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jeton = await responseMessage.Content.ReadAsStringAsync();
                generatedToken = jeton;
            }
            else
            {
                generatedToken = "";
            }
            

            return RedirectToAction("Index");
        }
        
        
        public async Task<IActionResult> AdminPage() {

            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("WebServiceAddress"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", generatedToken);
            
            var responseMessage = await _httpClient.GetAsync("member/JoinSuccess");
            if (responseMessage.IsSuccessStatusCode)
            {
                var serverMessage = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.Message = serverMessage;
            }
            else
            {
                ViewBag.Message = "Bye Bye Love";
            }



            return View();
        }

    }
}
