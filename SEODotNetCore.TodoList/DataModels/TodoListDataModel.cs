namespace SEODotNetCore.TodoList.DataModels
{
    public class TodoListDataModel
    {
        public int TaskId { get; set; }

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int CategoryId { get; set; }

        public byte  PriorityLevel { get; set; }
        public string Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }

        public int ForeignKey { get; set; }

        public int TaskCategory { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
