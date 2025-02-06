namespace ProjeBlog.Models.LogModels
{
    public class VisitorsLog : BaseEntity
    {
        public string ip {  get; set; }
        public string country { get; set; }
        public string LogType { get; set; }
    }
}
