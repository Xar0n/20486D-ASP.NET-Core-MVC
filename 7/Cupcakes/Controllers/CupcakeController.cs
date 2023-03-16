using System.IO;
using System.Threading.Tasks;
using Cupcakes.Models;
using Cupcakes.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cupcakes.Controllers
{
    public class CupcakeController : Controller
    {
        private ICupcakeRepository _repository;
        private IHostingEnvironment _environment;

        public CupcakeController(ICupcakeRepository repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetCupcakes());
        }

        public async Task<IActionResult> Details(int id)
        {
            var cupcake = await _repository.GetCupcakeById(id);

            if (cupcake is null) return NotFound();

            return View(cupcake);
        }

        private void PopulateBakeriesDropDownList(int? selectedBakery = null)
        {
            var bakeries = _repository.PopulateBakeriesDropDownList();
            ViewBag.BakeryID = new SelectList(bakeries.AsNoTracking(), "BakeryId", "BakeryName", selectedBakery);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateBakeriesDropDownList();

            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(Cupcake cupcake)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateCupcake(cupcake);
                return RedirectToAction(nameof(Index));
            }

            PopulateBakeriesDropDownList(cupcake.BakeryId);

            return View(cupcake);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Cupcake cupcake = await _repository.GetCupcakeById(id);
            if (cupcake is null) return NotFound();

            PopulateBakeriesDropDownList(cupcake.BakeryId);

            return View(cupcake);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var cupcakeToUpdate = await _repository.GetCupcakeById(id);
            bool isUpdated = await TryUpdateModelAsync<Cupcake>(
                cupcakeToUpdate,
                "",
                c => c.BakeryId,
                c => c.CupcakeType,
                c => c.Description,
                c => c.GlutenFree,
                c => c.Price);

            if (isUpdated)
            {
                await _repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            PopulateBakeriesDropDownList(cupcakeToUpdate.BakeryId);

            return View(cupcakeToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cupcake = await _repository.GetCupcakeById(id);

            if (cupcake == null) return NotFound();

            return View(cupcake);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteCupcake(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetImage(int id)
        {
            Cupcake requestedCupcake = await _repository.GetCupcakeById(id);
            if (requestedCupcake != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\images\\";
                string fullPath = webRootpath + folderPath + requestedCupcake.ImageName;
                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, requestedCupcake.ImageMimeType);
                }
                else
                {
                    if (requestedCupcake.PhotoFile.Length > 0)
                    {
                        return File(requestedCupcake.PhotoFile, requestedCupcake.ImageMimeType);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}