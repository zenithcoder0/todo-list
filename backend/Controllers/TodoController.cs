using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models;
using TodoListApp.Services;

namespace TodoListApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll() {
            return _todoService.GetAll();
        }

        [HttpGet("{id}")]

        public ActionResult<TodoItem> GetById(int id) {
            var todo = _todoService.GetById(id);
            if (todo is null) return NotFound();
            return todo;
        }

        [HttpPost]
        public ActionResult<TodoItem> Add([FromBody] TodoCreateRequest request) {
            var newTodo = _todoService.Add(request.Title);
            return CreatedAtAction(nameof(GetById), new { id = newTodo.id }, newTodo);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem updated) {
            var success = _todoService.Update(id, updated.Title, updated.IsCompleted);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _todoService.Delete(id);
            return success ? NoContent() : NotFound();
        }

    }
}