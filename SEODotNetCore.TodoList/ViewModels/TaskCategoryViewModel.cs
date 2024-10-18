namespace SEODotNetCore.TodoList.ViewModels
{
    public class TaskCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public bool DeleteFlag { get; set; }
    }
}
    