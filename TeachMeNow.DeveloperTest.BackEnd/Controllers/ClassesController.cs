using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers
{
    public class ClassesController : ApiController
    {
        // GET: api/Classes
        public IEnumerable<Class> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/Classes/5
        public Class Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Classes
        public void Post([FromBody]Class value)
        {
        }

        // PUT: api/Classes/5
        public void Put(int id, [FromBody]Class value)
        {
        }

        // DELETE: api/Classes/5
        public void Delete(int id)
        {
        }
    }
}
