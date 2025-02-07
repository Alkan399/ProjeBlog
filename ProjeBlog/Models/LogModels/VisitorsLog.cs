namespace ProjeBlog.Models.LogModels
{
    public class VisitorsLog : BaseEntity
    {
        public VisitorsLog()
        {
            ip = "";
            country = "";
            LogType = "Not Specific";
        }
        public string ip {  get; set; }
        public string country { get; set; }
        public string LogType { get; set; }
    }
}
