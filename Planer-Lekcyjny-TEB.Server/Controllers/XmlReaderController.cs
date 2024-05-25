using Microsoft.AspNetCore.Mvc;

namespace Planer_Lekcyjny_TEB.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class XmlReaderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"DummyData\Plan.xml");
            var xml = System.IO.File.ReadAllText(path);
            return Content(xml, "application/xml");
        }
    }
}
