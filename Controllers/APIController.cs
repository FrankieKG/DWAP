using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
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



        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = context.Organizations.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(result);
        }
        

        //http://www.webbadress.com/api/GetAllAtlasPartnerskap

        [HttpGet]
        public JsonResult GetAllAtlasPartnerskap()
        {

            //Returnerar gulmarkerad data för alla rapporter i Atlas Partnerskap

            return null;
        }

        //http://www.webbadress.com/api/GetRangeAtlasPartnerskap

        [HttpGet]
        public JsonResult GetRangeAtlasPartnerskap( int from, int to)
        {

            //Returnerar gulmarketad data för specika år inom Atlas Partnerskap

            return null;
        }


        [HttpGet]
        public JsonResult GetDnrAtlasPartnerskap(string dnr)
        {

            var results = repo.GetAtlasPartnerskapDnr(dnr);

            if(results == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(results, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
        }










        [HttpGet]
        public JsonResult GetAllAtlasPraktik()
        {


            return null;
        }







        [HttpGet]
        public JsonResult GetAllProgramstatistikMFSStipendier()
        {


            return null;
        }


        [HttpGet]
        public JsonResult GetAllMobilitetsstatistikMFSStipendier()
        {


            return null;
        }



    }
}
