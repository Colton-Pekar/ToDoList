using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.ListComponents;

namespace ToDoList.Pages.List
{
    public class EditModel : PageModel
    {
        private ApplicationDBContext _db;

        public EditModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ListComponents.ListComponents List { get; set; }

        public async Task OnGet(int id)
        {
            List = await _db.ListComponents.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var ListFromDb = await _db.ListComponents.FindAsync(List.ID);
                ListFromDb.ItemToDo = List.ItemToDo;
                ListFromDb.CompletedBy = List.CompletedBy;
                ListFromDb.ItemComplete = List.ItemComplete;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
