using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using ToDoList.ListComponents;

namespace ToDoList.Pages.List
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        private readonly HttpClient _client;

        public string itemfromBoss { get; set; }
        public string item { get; set; }
        public string completedby { get; set; }
        public bool readfromAPI { get; set; }
        

        public CreateModel(ApplicationDBContext db, HttpClient client)
        {
            _db = db;
            _client = client;
        }

        [BindProperty]
        public ListComponents.ListComponents List { get; set; }
        [BindProperty]
        public string index { get; set; }
        
        public int tasksize;
        public async void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            if (readfromAPI)
            {
                if (ModelState.IsValid)
                {
                    await _db.ListComponents.AddAsync(List);
                    await _db.SaveChangesAsync();

                    return RedirectToPage("Index");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {

                    var response = await _client.GetAsync("http://localhost:8080/toDoItems");
                    itemfromBoss = await response.Content.ReadAsStringAsync();

                    JArray obj = JArray.Parse(itemfromBoss);
                    List<string> itemList = new List<string>();
                    List<string> completeList = new List<string>();
                                       
                    for (int i = 0; i<obj.Count; i++) { 
                        item = obj[i]["itemToDo"].ToString();
                        completedby = obj[i]["completedBy"].ToString();
                        itemList.Add(item);
                        completeList.Add(completedby);

                        ListComponents.ListComponents newL = new ListComponents.ListComponents();
                        newL.ItemToDo = itemList[i];
                        newL.CompletedBy = completeList[i];
                        _db.ListComponents.Add(newL);
                    }
                    await _db.SaveChangesAsync();

                    return RedirectToPage("Index");
                }
                else
                {
                    return Page();
                }
            }
        }

    }
}
