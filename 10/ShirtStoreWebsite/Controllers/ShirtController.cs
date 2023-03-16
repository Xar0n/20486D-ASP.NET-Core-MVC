using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ShirtStoreWebsite.Models;
using ShirtStoreWebsite.Services;
using System;

namespace ShirtStoreWebsite.Controllers
{
    public class ShirtController : Controller
    {
        private readonly ILogger _logger;
        private readonly IShirtRepository _repository;

        public ShirtController(IShirtRepository repository, ILogger<ShirtController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var shirts = _repository.GetShirts();
            return View(shirts);
        }

        public IActionResult AddShirt(Shirt shirt)
        {
            _repository.AddShirt(shirt);
            _logger.LogDebug($"A {shirt.Color} shirt of size {shirt.Size} " +
                $"with a price of {shirt.GetFormattedTaxedPrice()} was added successfully.");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _repository.RemoveShirt(id);
                _logger.LogDebug($"A shirt with id {id} was removed successfully.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while trying " +
                    $"to delete shirt with id of {id}.");
                throw ex;
            }
        }
    }
}