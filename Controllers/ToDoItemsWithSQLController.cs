using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.DAL;
using WebAPIDemo.Model;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsWithSQLController : ControllerBase
    {
        [HttpGet]

        public ActionResult<ToDoItem> GetTodoitem()
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText =
                @"SELECT Id, Name, isComplete, Secret FROM Todoitems";
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return NotFound();
            return new ToDoItem
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                IsComplete = reader.GetBoolean(2),
                Secret = reader.IsDBNull(3) ? null : reader.GetString(3),
            };
        }

        [HttpPost]

        public ActionResult<ToDoItem> PostTodoitem(ToDoItem item)
        {
            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText =
                @"INSERT INTO Todoitems (Name, IsComplete, Secret) VALUES ($name, $iscomplete, $secret); SELECT last_insert_rowid();";
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("IsComplete", item.IsComplete);
            cmd.Parameters.AddWithValue("$secret", item.Secret);

            item.Id = Convert.ToInt32(cmd.ExecuteScalar());
            return CreatedAtAction(nameof(GetTodoitem), new { id = item.Id }, item);
        }

        [HttpPut("id")]
        public IActionResult PutTodoitem(int id, ToDoItem item)
        {
            if (id != item.Id)
                return BadRequest();

            using var conn = Database.GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText =
                @"UPDATE Todoitems SET Name = $name, IsComplete, Secret = $secret WHERE Id = $id";

            cmd.Parameters.AddWithValue("$id", item.Id);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$isCpmlete", item.IsComplete);
            cmd.Parameters.AddWithValue("$secret", item.Secret);

            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
                return NotFound();
            return NoContent();
        }
    }
}

