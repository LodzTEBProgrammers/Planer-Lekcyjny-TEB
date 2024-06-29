using Microsoft.AspNetCore.Mvc;
using Planer_Lekcyjny_TEB.Server.Classes;
using Planer_Lekcyjny_TEB.Server.Services;
using System.Xml.Linq;

namespace Planer_Lekcyjny_TEB.Server.Controllers;

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
        List<Lesson>? lessons = _lessonService.GetLessons();

        // Parse classrooms
        Dictionary<string, string>? classrooms = doc.Descendants("classroom")
            .ToDictionary(c => (string)c.Attribute("id"),
                c => (string)c.Attribute("name"));

        // Parse periods
        List<Period>? periods = new List<Period>();
        foreach (XElement? p in doc.Descendants("period"))
        {
            Period? period = new Period
            {
                Name = (string)p.Attribute("name"),
                PeriodNumber = (int)p.Attribute("period"),
                StartTime = TimeSpan.Parse((string)p.Attribute("starttime")),
                EndTime = TimeSpan.Parse((string)p.Attribute("endtime"))
            };
            periods.Add(period);
        }

        // Get the current time
        TimeSpan now = DateTime.Now.TimeOfDay;

        // Parse days
        Dictionary<string, string>? days = doc.Descendants("daysdef")
            .ToDictionary(d => (string)d.Attribute("days"),
                d => (string)d.Attribute("name"));

        // Get the current day of the week
        int currentDayOfWeek = (int)DateTime.Now.DayOfWeek;

        // Get the current day string
        string? currentDayString = new string('0', currentDayOfWeek - 1) + "1" +
            new string('0', 5 - currentDayOfWeek);

        // Get the current day name
        string? currentDayName = days.ContainsKey(currentDayString)
            ? days[currentDayString]
            : null;


        // Find the current or next period
        Period? currentOrNextPeriod =
            periods.FirstOrDefault(p => p.StartTime <= now && p.EndTime > now)
        ?? periods.FirstOrDefault(p => p.StartTime > now);

        // If there is a current or next lesson 
        if (currentOrNextPeriod != null)
        {
            // Get the current or next cards
            List<Card>? currentOrNextCards = doc.Descendants("card")
                .Where(c =>
                    (int)c.Attribute("period") ==
                    currentOrNextPeriod.PeriodNumber &&
                    ((string)c.Attribute("days")).Substring(
                        currentDayOfWeek - 1, 1) == "1")
                .Select(c => new Card
                {
                    Class = lessons.FirstOrDefault(l =>
                        l.Id == (string)c.Attribute("lessonid"))?.Class,
                    Lesson = lessons.FirstOrDefault(l =>
                        l.Id == (string)c.Attribute("lessonid"))?.Subject,
                    Teacher = lessons.FirstOrDefault(l =>
                        l.Id == (string)c.Attribute("lessonid"))?.Teacher,
                    Classroom =
                        string.IsNullOrEmpty(
                            (string)c.Attribute("classroomids"))
                            ? null
                            : classrooms.ContainsKey(
                                (string)c.Attribute("classroomids"))
                                ? classrooms[
                                    (string)c.Attribute("classroomids")]
                                : null,
                    StartTime = currentOrNextPeriod.StartTime.ToString(),
                    EndTime = currentOrNextPeriod.EndTime.ToString(),
                    Day = currentDayName
                })
                .OrderBy(c => c.Class) // Sort by class
                .ToList();

            return Ok(currentOrNextCards);
        } else
        {
            // Parse cards
            List<Card>? cards = doc.Descendants("card")
                .Select(c =>
                {
                    int? periodNumber = (int?)c.Attribute("period");
                    Period? period =
                        periods.FirstOrDefault(p =>
                            p.PeriodNumber == periodNumber);
                    string? lessonId = (string)c.Attribute("lessonid");
                    Lesson? lesson =
                        lessons.FirstOrDefault(l => l.Id == lessonId);
                    string? classroomId = (string)c.Attribute("classroomids");
                    string? classroom = string.IsNullOrEmpty(classroomId)
                        ?
                        null
                        : classrooms.ContainsKey(classroomId)
                            ? classrooms[classroomId]
                            : null;

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

            DateTime currentTime = DateTime.Now;

            List<Card>? currentCards = cards
                .Where(c =>
                    DateTime.Parse(c.StartTime) <= currentTime &&
                    DateTime.Parse(c.EndTime) >= currentTime)
                .ToList();

            // If there are no current cards, return a message
            if (!currentCards.Any())
                return Ok(new
                {
                    message = "Wszystkie lekcje na dzisiaj zakończyły się!"
                });

            return Ok(currentCards);
        }
    }
}
