using Planer_Lekcyjny_TEB.Server.Classes;
using System.Xml.Linq;

namespace Planer_Lekcyjny_TEB.Server.Services
{
    public class LessonService
    {
        public List<Lesson> GetLessons()
        {
            // Load XML document
            XDocument doc = XDocument.Load("DummyData/Plan.xml");

            // Parse classes
            var classIds = doc.Descendants("class")
                .Select(c => new Class
                {
                    Id = (string)c.Attribute("id"),
                    Name = (string)c.Attribute("name"),
                })
                .ToList();

            // Parse classroom
            var classRooms = doc.Descendants("classroom")
                .Select(c => new Classroom
                {
                    Id = (string)c.Attribute("id"),
                    Name = (string)c.Attribute("name"),
                })
                .ToList();

            // Parse subjects
            var subjects = doc.Descendants("subject")
                .Select(s => new Subject
                {
                    Id = (string)s.Attribute("id"),
                    Name = (string)s.Attribute("name"),
                })
                .ToList();

            // Parse teachers
            var teachers = doc.Descendants("teacher")
                .Select(t => new Teacher
                {
                    Id = (string)t.Attribute("id"),
                    FirstName = (string)t.Attribute("firstname"),
                    LastName = (string)t.Attribute("lastname"),
                })
                .ToList();

            // Parse periods
            var periods = doc.Descendants("period")
                .Select(p => new Period
                {
                    Name = (string)p.Attribute("name"),
                    PeriodNumber = (int)p.Attribute("period"),
                    StartTime = TimeSpan.Parse((string)p.Attribute("starttime")),
                    EndTime = TimeSpan.Parse((string)p.Attribute("endtime")),
                })
                .ToList();

            var lessons = doc.Descendants("lesson")
                .Select(l => new Lesson
                {
                    Id = (string)l.Attribute("id"),

                    Class = classIds.Where(c => c.Id == (string)l.Attribute("classids"))
                        .Select(c => c.Name)
                        .FirstOrDefault(),

                    Subject = subjects.Where(s => s.Id == (string)l.Attribute("subjectid"))
                        .Select(s => s.Name)
                        .FirstOrDefault(),

                    Classroom = ((string)l.Attribute("classroomids")).Split(',')
                        .Select(id => classRooms.FirstOrDefault(c => c.Id == id)?.Name)
                        .Where(name => name != null)
                        .ToList(),
                    PeriodsPerCard = (int)l.Attribute("periodspercard"),
                    PeriodsPerWeek = (double)l.Attribute("periodsperweek"),
                    Teacher = teachers.Where(t => t.Id == (string)l.Attribute("teacherids"))
                        .Select(t => $"{t.FirstName} {t.LastName}")
                        .FirstOrDefault(),
                    TermsDefId = (string)l.Attribute("termsdefid"),
                    WeeksDefId = (string)l.Attribute("weeksdefid"),
                    DaysDefId = (string)l.Attribute("daysdefid"),
                    // Find the corresponding period and assign the start and end times
                    StartTime = periods.Where(p => l.Attribute("period") != null && p.PeriodNumber == (int)l.Attribute("period"))
                        .Select(p => p.StartTime)
                        .FirstOrDefault(),
                    EndTime = periods.Where(p => l.Attribute("period") != null && p.PeriodNumber == (int)l.Attribute("period"))
                        .Select(p => p.EndTime)
                        .FirstOrDefault(),
                })
                .ToList();

            return lessons;
        }
    }
}
