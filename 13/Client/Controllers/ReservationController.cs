using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Client.Models;

namespace Client.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateRestaurantBranchesDropDownListAsync();
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePostAsync(OrderTable orderTable)
        {
            var httpclient = _httpClientFactory.CreateClient();
            var response = await httpclient.PostAsJsonAsync("http://localhost:54517/api/Reservation", orderTable);
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadAsAsync<OrderTable>();
                return RedirectToAction("ThankYouAsync", new { orderId = order.Id });
            }
            else return View("Error");
        }

        private async Task PopulateRestaurantBranchesDropDownListAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:54517");
            var response = await httpClient.GetAsync("api/RestaurantBranches");
            if (response.IsSuccessStatusCode)
            {
                var restaurantBranches = await response.Content
                    .ReadAsAsync<IEnumerable<RestaurantBranch>>();
                ViewBag.RestaurantBranches = new SelectList(restaurantBranches, "Id", "City");
            }
        }

        public async Task<IActionResult> ThankYouAsync(int orderId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:54517");
            var response = await httpClient.GetAsync("api/Reservation/" + orderId);
            if (response.IsSuccessStatusCode)
            {
                var orderResult = await response.Content.ReadAsAsync<OrderTable>();
                return View(orderResult);
            }
            else return View("Error");
        }
    }
}
