namespace Planer_Lekcyjny_TEB.Server.Classes
{
    public class Lesson
    {
        public string Id { get; set; }
        public string ClassIds { get; set; }
        public string SubjectId { get; set; }
        public int PeriodsPerCard { get; set; }
        public double PeriodsPerWeek { get; set; }
        public string TeacherIds { get; set; }
        public string ClassroomIds { get; set; }
        public string GroupIds { get; set; }
        public string Capacity { get; set; }
        public string SeminarGroup { get; set; }
        public string TermsDefId { get; set; }
        public string WeeksDefId { get; set; }
        public string DaysDefId { get; set; }
        public string PartnerId { get; set; }
    }
}
