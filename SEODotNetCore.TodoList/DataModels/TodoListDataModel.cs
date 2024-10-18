namespace SEODotNetCore.TodoList.DataModels
{
    public class TodoListDataModel
    {
        public int TaskId { get; set; }

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int CategoryId { get; set; }

        public int PriorityLevel { get; set; }
        public string Status { get; set; }

        public string date { get; set; }

        public string CreatedDate { get; set; }
        public string CompletedDate { get; set; }

        public int ForeignKey { get; set; }

        public int TaskCategory { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
