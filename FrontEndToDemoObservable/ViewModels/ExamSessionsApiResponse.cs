namespace FrontEndToDemoObservable.ViewModels
{
    public class AtcExamSessions
    {
        public SessionGrouping Sessions { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SessionGrouping
    {
        public IEnumerable<ExamSession> OpenSessions { get; set; }
        public IEnumerable<ExamSession> ClosedSessions { get; set; }
    }

    public class ExamSession
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SessionCode { get; set; }
        public string Status { get; set; }
        public string AtcName { get; set; }
        public string Name { get; set; }
        public Guid AtcId { get; set; }
    }
}
