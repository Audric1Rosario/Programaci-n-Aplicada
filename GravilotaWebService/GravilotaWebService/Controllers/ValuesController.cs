using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GravilotaModel;

namespace GravilotaWebService.Controllers
{

    public class ValuesController : ApiController
    {
        BDISC210Entities dbContext = new BDISC210Entities();
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            int i;
            return "value";

        }

        // POST api/values
        public int Post([FromBody]GravilotaScore newScore)
        {
            dbContext.GravilotaScore.Add(newScore);
            return dbContext.SaveChanges();
        }

        // PUT api/values/5
        public int Put(int id, [FromBody]GravilotaScore newScore)
        {
            dbContext.GravilotaScore.Add(newScore);
            return dbContext.SaveChanges();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
