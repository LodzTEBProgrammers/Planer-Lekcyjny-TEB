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
            var periods = new List<Period>();
            foreach (var p in doc.Descendants("period"))
            {
                var period = new Period
                {
                    Name = (string)p.Attribute("name"),
                    PeriodNumber = (int)p.Attribute("period"),
                    StartTime = TimeSpan.Parse((string)p.Attribute("starttime")),
                    EndTime = TimeSpan.Parse((string)p.Attribute("endtime"))
                };
                periods.Add(period);
            }

            // Get the current time
            var now = DateTime.Now.TimeOfDay;

            // Find the current or next period
            var currentOrNextPeriod = periods.FirstOrDefault(p => p.StartTime <= now && p.EndTime > now)
                                      ?? periods.FirstOrDefault(p => p.StartTime > now);

            // If there is a next lesson 
            if (currentOrNextPeriod != null)
            {
                // Get the next cards
                var nextCards = doc.Descendants("card")
                    .Where(c => (int)c.Attribute("period") == currentOrNextPeriod.PeriodNumber)
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
                        StartTime = currentOrNextPeriod.StartTime.ToString(),
                        EndTime = currentOrNextPeriod.EndTime.ToString()
                    })
                    .ToList();

                return Ok(nextCards);
            }
            else
            {

                // Parse cards
                var cards = doc.Descendants("card")
                    .Select(c =>
                    {
                        var periodNumber = (int?)c.Attribute("period");
                        var period = periods.FirstOrDefault(p => p.PeriodNumber == periodNumber);
                        var lessonId = (string)c.Attribute("lessonid");
                        var lesson = lessons.FirstOrDefault(l => l.Id == lessonId);
                        var classroomId = (string)c.Attribute("classroomids");
                        var classroom = string.IsNullOrEmpty(classroomId) ? null : classrooms.ContainsKey(classroomId) ? classrooms[classroomId] : null;

                        return new Card
                        {
                            Class = lesson?.Class,
                            Lesson = lesson?.Subject,
                            Teacher = lesson?.Teacher,
                            Classroom = classroom,
                            StartTime = period?.StartTime.ToString(),
                            EndTime = period?.EndTime.ToString()
                        };
                    })
                    .ToList();

                var currentTime = DateTime.Now;

                var currentCards = cards
                    .Where(c => DateTime.Parse(c.StartTime) <= currentTime && DateTime.Parse(c.EndTime) >= currentTime)
                    .ToList();

                return Ok(currentCards);
            }
        }
    }
}
