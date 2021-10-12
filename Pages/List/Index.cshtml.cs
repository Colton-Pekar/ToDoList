using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.ListComponents;

namespace ToDoList.Pages.List
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
        }

        public IEnumerable<ListComponents.ListComponents> ListComponent { get; set; }
        public async Task OnGet()
        {
            ListComponent = await _db.ListComponents.ToListAsync();
        }
    }
}
