using Microsoft.AspNetCore.Mvc;
using Planer_Lekcyjny_TEB.Server.Classes;
using System.Xml.Linq;

namespace Planer_Lekcyjny_TEB.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Lesson>> Get()
        {
            // Load XML document
            XDocument doc = XDocument.Load("DummyData/Plan.xml");

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

            // Parse lessons
            var lessons = doc.Descendants("lesson")
                .Select(l => new Lesson
                {
                    Id = (string)l.Attribute("id"),
                    ClassIds = (string)l.Attribute("classids"),
                    SubjectName = subjects.Where(s => s.Id == (string)l.Attribute("subjectid"))
                        .Select(s => s.Name)
                        .FirstOrDefault(),
                    PeriodsPerCard = (int)l.Attribute("periodspercard"),
                    PeriodsPerWeek = (double)l.Attribute("periodsperweek"),
                    TeacherIds = (string)l.Attribute("teacherids"),
                    TeacherName = teachers.Where(t => t.Id == (string)l.Attribute("teacherids"))
                                            .Select(t => $"{t.FirstName} {t.LastName}")
                                            .FirstOrDefault(),
                    ClassroomIds = (string)l.Attribute("classroomids"),
                    GroupIds = (string)l.Attribute("groupids"),
                    Capacity = (string)l.Attribute("capacity"),
                    SeminarGroup = (string)l.Attribute("seminargroup"),
                    TermsDefId = (string)l.Attribute("termsdefid"),
                    WeeksDefId = (string)l.Attribute("weeksdefid"),
                    DaysDefId = (string)l.Attribute("daysdefid"),
                    PartnerId = (string)l.Attribute("partner_id")
                })
                .ToList();

            return Ok(lessons);
        }
    }
}
