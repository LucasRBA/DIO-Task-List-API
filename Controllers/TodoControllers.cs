using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly OrganizerContext _context;

        public TodoController(OrganizerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = _context.Todos.Find(id);
            if(todo == null) {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var todo = _context.Todos.ToList();
            return Ok(todo);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var todo = _context.Todos.Where(x=> x.Title.ToUpper().Contains(title.ToUpper())).ToList();
            return Ok(todo);
        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime startDate)
        {
            var todo = _context.Todos.Where(x => x.StartDate.Date == startDate.Date);
            return Ok(todo);
        }

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(TodoStatusEnum status)
        {
            var todo = _context.Todos.Where(x => x.Status == status);
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (todo.StartDate == DateTime.MinValue)
                todo.StartDate = DateTime.UtcNow;

            if(todo.Title == null || todo.Title.Equals(" ") || todo.Title.Equals("string")) 
                return BadRequest(new {Error = "Title can't be null or generic like default Swagger string value... "});

            if(todo.Deadline == null) 
                return BadRequest(new { Error = "Task/Todo deadline can't be null..." });

            _context.Todos.Add(todo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Todo todo)
        {
            var todoInDatabase = _context.Todos.Find(id);

            if (todoInDatabase == null)
                return NotFound();

            if (todo.StartDate == DateTime.MinValue)
                todo.StartDate= todoInDatabase.StartDate;

            if(todo.Title == null || todo.Title.Equals("") ||todo.Title.Equals(" ") || todo.Title.Equals("string")) 
                todo.Title = todoInDatabase.Title;

            if(todo.Deadline == null || todo.Deadline == DateTime.MinValue) 
                todo.Deadline = todoInDatabase.Deadline;

            if(todo.Description == null || todo.Description.Equals("") ||todo.Description.Equals(" ") || todo.Description.Equals("string")) 
                todo.Description = todoInDatabase.Description;
 

            todoInDatabase.Title = todo.Title;
            todoInDatabase.Description = todo.Description;
            todoInDatabase.StartDate = todo.StartDate;
            todoInDatabase.Deadline = todo.Deadline;
            todoInDatabase.Status = todo.Status;

            _context.Todos.Update(todoInDatabase);
            _context.SaveChanges();
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todoInDatabase = _context.Todos.Find(id);

            if (todoInDatabase == null)
                return NotFound();

            _context.Todos.Remove(todoInDatabase);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
