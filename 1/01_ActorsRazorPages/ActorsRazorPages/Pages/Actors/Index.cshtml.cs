using ActorsRazorPages.Interfaces;
using ActorsRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActorsRazorPages.Pages.Actors
{
    public class IndexModel : PageModel
    {
        private readonly IData _data;
        public IList<Actor> Actors { get; set; }

        public IndexModel(IData data)
        {
            _data = data;
        }

        public void OnGet()
        {
            Actors = _data.ActorsInitializeData();
        }
    }
}
