namespace SEODotNetCore.TodoList.ViewModels
{
    public class TodoListViewModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Description{ get; set; }
        public int CategoryId { get; set; }

        public byte PriorityLevel { get; set; }
        public string? Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public int ForeignKey { get; set; }

        public int TaskCategory { get; set; }

        public bool DeleteFlag { get; set; }
    }
}

