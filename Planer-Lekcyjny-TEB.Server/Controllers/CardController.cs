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

            // Parse cards
            var cards = doc.Descendants("card")
                .Select(c => new Card
                {
                    LessonName = lessons.FirstOrDefault(l => l.Id == (string)c.Attribute("lessonid"))?.Subject,
                    ClassroomIds = (string)c.Attribute("classroomids"),
                    Period = (int)c.Attribute("period"),
                    Weeks = (int)c.Attribute("weeks"),
                    Terms = (int)c.Attribute("terms"),
                    Days = (int)c.Attribute("days"),
                })
                .ToList();

            return Ok(cards);
        }
    }
}
