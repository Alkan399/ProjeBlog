namespace ProjeBlog.Models.LogModels
{
    public class OperationsLog : BaseEntity
    {
        public OperationsLog()
        {
            Ip = "Null";
            UserId = 0;
            Operation = "Null";
            ModelName = "Null";
            Message = "Null";
        }
        public string Ip { get; set; }
        public int? UserId { get; set; }
        public string Operation { get; set; }
        public string ModelName {get; set; }
        public string Message { get; set; }
    }
}
