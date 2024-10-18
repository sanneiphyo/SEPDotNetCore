namespace SEODotNetCore.TodoList.ViewModels
{
    public class TodoListViewModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Description{ get; set; }
        public int CategoryId { get; set; }

        public int PriorityLevel { get; set; }
        public string? Status { get; set; }

        public string? date { get; set; }

        public string? CreatedDate { get; set; }
        public string? CompletedDate { get; set; }

        public int ForeignKey { get; set; }

        public int TaskCategory { get; set; }

        public bool DeleteFlag { get; set; }
    }
}

