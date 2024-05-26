namespace Planer_Lekcyjny_TEB.Server.Classes
{
    public class Lesson
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
        public List<string> Classroom { get; set; }
        public string Teacher { get; set; }
        public int PeriodsPerCard { get; set; }
        public double PeriodsPerWeek { get; set; }
        public string TermsDefId { get; set; }
        public string WeeksDefId { get; set; }
        public string DaysDefId { get; set; }
        public TimeSpan StartTime { get; set; } // New property
        public TimeSpan EndTime { get; set; } // New property
    }
}
