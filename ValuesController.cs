using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            var retval = new { key1 = "value1", key2 = "value2" };
            return Request.CreateResponse(HttpStatusCode.OK,retval);
        }
    }
}
