using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OBAPI.Domain.ViewModels;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MvcClient.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private string baseURL = "http://localhost:5001/api";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			var token = await HttpContext.GetTokenAsync("access_token");
			var toDate = System.DateTime.UtcNow;
			var fromDate = toDate.AddDays(-14);

			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await client.GetAsync($"{baseURL}/extrato?from={fromDate:yyyy-MM-dd}&to={toDate:yyyy-MM-dd}");

			try
			{
				response.EnsureSuccessStatusCode();
				// Handle success
				
			}
			catch (HttpRequestException)
			{
				// Handle error
			}
			
			ViewBag.Json = await response.Content.ReadAsStringAsync();
			return View();
		}

		public IActionResult Claims()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Deposito()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Deposito(CreditViewModel deposito)
		{
			var token = await HttpContext.GetTokenAsync("access_token");

			var client = new HttpClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var json = JsonConvert.SerializeObject(deposito);

			var stringContent = new StringContent(json);

			var response = await client.PostAsync($"{baseURL}/deposito?description={deposito.Description}&amount={deposito.Amount}", stringContent);

			var result = "";

			try
			{
				response.EnsureSuccessStatusCode();
				// Handle success
				result = await response.Content.ReadAsStringAsync();
			}
			catch (HttpRequestException)
			{
				// Handle error
				ViewBag.Json = await response.Content.ReadAsStringAsync();
				return View();
			}

			ViewBag.Json = result;

			return View();
		}

		[HttpGet]
		public IActionResult Saque()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Saque(CreditViewModel deposito)
		{
			var token = await HttpContext.GetTokenAsync("access_token");

			var client = new HttpClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var json = JsonConvert.SerializeObject(deposito);

			var stringContent = new StringContent(json);

			var response = await client.PostAsync($"{baseURL}/saque?description={deposito.Description}&amount=-{deposito.Amount}", stringContent);

			var result = "";

			try
			{
				response.EnsureSuccessStatusCode();
				// Handle success
				result = await response.Content.ReadAsStringAsync();
			}
			catch (HttpRequestException)
			{
				// Handle error
				ViewBag.Json = await response.Content.ReadAsStringAsync();
				return View();
			}

			ViewBag.Json = result;

			return View();
		}


		public async Task<IActionResult> CallApi()
		{
			var accessToken = await HttpContext.GetTokenAsync("access_token");

			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			var content = await client.GetStringAsync("http://localhost:5001/identity");

			ViewBag.Json = JArray.Parse(content).ToString();
			return View("json");
		}

		public IActionResult Logout()
		{
			return SignOut("Cookies", "oidc");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}