using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api")]
    [ApiController]

    public class APIController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IRepository repo;


        public APIController(ApplicationDbContext context, IRepository repo)
        {
            this.context = context;
            this.repo = repo;
        }




        [Route("GetDnrAtlasPartnerskap")]
        public JsonResult GetDnrAtlasPartnerskap(string dnr)
        {

            var results = repo.GetAtlasPartnerskapDnr(dnr);

            if(results == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(results);
        }


        [Route("GetDnrAtlasPraktik")]
        public JsonResult GetDnrAtlasPraktik(string dnr)
        {

            var results = repo.GetAtlasPraktikDnr(dnr);

            if (results == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(results);
        }





    }
}
