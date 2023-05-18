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


        //Funkar!
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

        //Funkar!!!
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


        [Route("GetDnrMobilitetsstatistikMFSStipendier")]
        public JsonResult GetDnrMobilitetsstatistikMFSStipendier(string dnr)
        {
            var results = repo.GetMobilitetsstatistikMFSStipendierDnr(dnr);

            if (results == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(results);
        }

        [Route("GetDnrMFSStipendier")]
        public JsonResult GetDnrMFSStipendier(string dnr)
        {
            var results = repo.GetMFSStipendierDnr(dnr);

            if (results == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(results);
        }
    }
}
