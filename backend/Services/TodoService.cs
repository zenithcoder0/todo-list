using TodoListApp.Models;

namespace TodoListApp.Services
{
    public class TodoService
    {
        private readonly List<TodoItem> _todos = new();
        private int _nextId = 1;

        public List<TodoItem> GetAll()
        {
            return _todos;
        }

        public TodoItem? GetById(int id)
        {
            return _todos.FirstOrDefault(t => t.id == id);
        }

        public TodoItem Add(string title)
        {
            var todo = new TodoItem
            {
                id = _nextId++,
                Title = title,
                IsCompleted = false
            };

            _todos.Add(todo);
            return todo;
        }

        public bool Update(int id, string newTitle, bool isCompleted)
        {
            var todo = GetById(id);
            if (todo is null) return false;

            todo.Title = newTitle;
            todo.IsCompleted = isCompleted;
            return true;
        }

        public bool Delete(int id)
        {
            var todo = GetById(id);
            if (todo is null) return false;

            return _todos.Remove(todo);
        }
    }
}