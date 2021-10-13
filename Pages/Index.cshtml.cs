using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.ListComponents;

namespace ToDoList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ListComponents.ListComponents List { get; set; }
        public IEnumerable<ListComponents.ListComponents> ListComponent { get; set; }
        public async Task OnGet()
        {
            ListComponent = await _db.ListComponents.ToListAsync();
        }
        public async Task<IActionResult> OnPostComplete(int id)
        {
            var ListItem = await _db.ListComponents.FindAsync(id);
            if (ListItem == null)
            {
                return NotFound();
            }
            ListItem.ItemComplete = !ListItem.ItemComplete;
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");

        }
    }
}
