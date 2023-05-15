using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class APIController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public APIController(ApplicationDbContext context)
        {
            this.context = context;
        }



        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = context.Organizations.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(result);
        }





    }
}
