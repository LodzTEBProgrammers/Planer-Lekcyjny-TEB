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

            // Parse lessons
            var lessons = doc.Descendants("lesson")
                .Select(l => new Lesson
                {
                    Id = (string)l.Attribute("id"),
                    ClassIds = (string)l.Attribute("classids"),
                    SubjectId = (string)l.Attribute("subjectid"),
                    PeriodsPerCard = (int)l.Attribute("periodspercard"),
                    PeriodsPerWeek = (double)l.Attribute("periodsperweek"),
                    TeacherIds = (string)l.Attribute("teacherids"),
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
