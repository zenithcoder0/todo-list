namespace TodoListApp.Models
{
    public class TodoItem
    {
        public int id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}