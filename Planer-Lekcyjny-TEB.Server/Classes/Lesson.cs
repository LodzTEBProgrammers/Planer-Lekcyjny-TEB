﻿namespace Planer_Lekcyjny_TEB.Server.Classes
{
    public class Lesson
    {
        public string Id { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public List<string> ClassRoomNames { get; set; }
        public int PeriodsPerCard { get; set; }
        public double PeriodsPerWeek { get; set; }
        public string TeacherIds { get; set; }
        public string TeacherName { get; set; }
        public string GroupIds { get; set; }
        public string SeminarGroup { get; set; }
        public string TermsDefId { get; set; }
        public string WeeksDefId { get; set; }
        public string DaysDefId { get; set; }
    }
}
