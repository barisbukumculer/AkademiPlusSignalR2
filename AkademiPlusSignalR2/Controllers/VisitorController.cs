using AkademiPlusSignalR2.DAL.Context;
using AkademiPlusSignalR2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AkademiPlusSignalR2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {private readonly VisitorService _visitorService;

        public VisitorController(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpGet]
        public IActionResult CreateVisitor()
        {
            Random random= new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newVisitor = new Visitor
                    {
                        City=item,
                        VisitDate=DateTime.Now.AddDays(x),
                        CityVisitor=random.Next(1000, 5000)
                    };
                    _visitorService.SaveVisitor(newVisitor).Wait();
                    System.Threading.Thread.Sleep(1000);
                }
            });
            return Ok("Ziyaterçiler Eklendi.");
        }
        [HttpPost]
        public IActionResult CreateVisitor(Visitor visitor)
        {
            return Ok();
        }
    }
}
