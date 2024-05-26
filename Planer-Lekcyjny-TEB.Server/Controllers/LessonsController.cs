using Microsoft.AspNetCore.Mvc;
using Planer_Lekcyjny_TEB.Server.Classes;
using Planer_Lekcyjny_TEB.Server.Services;

namespace Planer_Lekcyjny_TEB.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public LessonsController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lesson>> Get()
        {
            var lessons = _lessonService.GetLessons();

            return Ok(lessons);
        }
    }
}
