using Microsoft.AspNetCore.Mvc;
using Planer_Lekcyjny_TEB.Server.Classes;
using Planer_Lekcyjny_TEB.Server.Services;
using System.Xml.Linq;

namespace Planer_Lekcyjny_TEB.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public CardController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> Get()
        {
            // Load XML document
            XDocument doc = XDocument.Load("DummyData/Plan.xml");

            // Get lessons
            var lessons = _lessonService.GetLessons();

            // Parse classrooms
            var classrooms = doc.Descendants("classroom")
                .ToDictionary(c => (string)c.Attribute("id"), c => (string)c.Attribute("name"));

            // Parse periods
            var periods = doc.Descendants("period")
                .ToDictionary(p => (int)p.Attribute("period"), p => new { StartTime = (string)p.Attribute("starttime"), EndTime = (string)p.Attribute("endtime") });


            // Parse cards
            var cards = doc.Descendants("card")
                .Select(c => new Card
                {
                    Class = lessons.FirstOrDefault(l => l.Id == (string)c.Attribute("lessonid"))?.Class,
                    Lesson = lessons.FirstOrDefault(l => l.Id == (string)c.Attribute("lessonid"))?.Subject,
                    Teacher = lessons.FirstOrDefault(l => l.Id == (string)c.Attribute("lessonid"))?.Teacher,
                    Classroom = string.IsNullOrEmpty((string)c.Attribute("classroomids"))
                        ? null
                        : classrooms.ContainsKey((string)c.Attribute("classroomids"))
                            ? classrooms[(string)c.Attribute("classroomids")]
                            : null,
                    StartTime = periods.ContainsKey((int)c.Attribute("period")) ? periods[(int)c.Attribute("period")].StartTime : null,
                    EndTime = periods.ContainsKey((int)c.Attribute("period")) ? periods[(int)c.Attribute("period")].EndTime : null,
                })
                .ToList();

            return Ok(cards);
        }
    }
}
