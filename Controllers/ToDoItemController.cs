using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Model;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private static long _nextId = 3;
        private static readonly List<ToDoItem> ToDoItems = new()
        {


            new ToDoItem
            {
                Id = 1,
                Name = "Learn.Net Core",
                IsComplete = true,
                Secret = "Secret 1",

            },

            new ToDoItem
            {
                Id = 2,
                Name = "Build Web Page",
                IsComplete = false,
                Secret = "Secret 2",
            }
        };

        [HttpGet]
        public ActionResult<List<ToDoItem>> GetToDoItems()
        {
            return ToDoItems;
        }

        [HttpGet("(id)")]
        public ActionResult<ToDoItem> GetToDoItem(long id)
        {
            var ToDoItem = ToDoItems.FirstOrDefault(t => t.Id == id);
            if (ToDoItem == null)
            {
                return NotFound();
            }
            return ToDoItem;
        }

        [HttpPost]
        public void PostToDoItem(ToDoItem todoitem)
        {
            todoitem.Id = _nextId++;
            ToDoItems.Add(todoitem);
        }

        [HttpDelete("(id)")]
        public void DeleteToDoItem(long id)
        {
            var todoitem = ToDoItems.FirstOrDefault(t => t.Id == id);
            ToDoItems.Remove(todoitem);
        }

        [HttpPut("(id)")]
        public void UpdateToDoItem(long id, ToDoItem update)
        {
            var todoitem = ToDoItems.FirstOrDefault(t => t.Id == id);
            todoitem.Name = update.Name;
            todoitem.IsComplete = update.IsComplete;
        }
            
    }
}
