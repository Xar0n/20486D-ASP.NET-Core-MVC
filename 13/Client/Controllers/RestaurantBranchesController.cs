using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Client.Models;

namespace Client.Controllers
{
    public class RestaurantBranchesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RestaurantBranchesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:54517");
            var response = await httpClient.GetAsync("api/RestaurantBranches");
            if (response.IsSuccessStatusCode)
            {
                var restaurantBranches = await response.Content.ReadAsAsync<IEnumerable<RestaurantBranch>>();
                return View(restaurantBranches);
            }
            else return View("Error");
        }
    }
}
