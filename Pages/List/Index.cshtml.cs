using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ToDoList.ListComponents;

namespace ToDoList.Pages.List
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        private readonly HttpClient _client;

        public string itemfromBoss { get; set; }
        public int tasksize { get; set; }

        public IndexModel(ApplicationDBContext db, HttpClient client)
        {
            _db = db;
            _client = client;
        }

        public IEnumerable<ListComponents.ListComponents> ListComponent { get; set; }
        public async Task OnGet()
        {
            ListComponent = await _db.ListComponents.ToListAsync();
            if (ModelState.IsValid)
            {
                var response = await _client.GetAsync("http://localhost:8080/toDoItems");
                itemfromBoss = await response.Content.ReadAsStringAsync();
                JArray objarr = JArray.Parse(itemfromBoss);
                tasksize = objarr.Count;
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var ListItem = await _db.ListComponents.FindAsync(id);
            if (ListItem == null)
            {
                return NotFound();
            }
            _db.ListComponents.Remove(ListItem);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");

        }
    }
}
