using AnimalsMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsMvc.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IData _tempData;

        public AnimalsController(IData tempData)
        {
            _tempData = tempData;
        }

        public IActionResult Index()
        {
            var animals = _tempData.AnimalsInitializeData();
            var indexViewModel = new IndexViewModel(animals);
            return View(indexViewModel);
        }

        public IActionResult Details(int? id)
        {
            var model = _tempData.GetAnimalById(id);
            if (model is null) return NotFound();
            return View(model);
        }
    }
}
