using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MapColoring.Controllers
{
    public class WebApiController : ApiController
    {
        [HttpGet]
        [Route("api/getFillColors/{mapId}/{colorsCount}")]
        public Result CSP(string mapID, int colorsCount)
        {
            CSP csp = new CSP();

            Result result = csp.GetResult(mapID, colorsCount);


            return result;
        }
    }
}
