using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GttApiWeb.Models;

namespace GttApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JiraController : ControllerBase
    {
        private readonly AppDBContext _context;

        public JiraController(AppDBContext context)
        {
            this._context = context;
        }

        // GET api/jira
        [HttpGet]
        public ActionResult<List<Jira>> Get()
        {
            return this._context.Jira.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult<Jira> Post([FromBody] Jira value)
        {

           /* Jira userExistencia = this._context.Jira.Where(
                        jira => jira.user_id == value.user_id).FirstOrDefault();*/

            Console.WriteLine("-------------------------------");
            this._context.Jira.Add(value);
            this._context.SaveChanges();
            return value;
            //Console.WriteLine("---------------"+value.Users.id);
            //return null;
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] Jira value)
        {
            Jira jira = this._context.Jira.Find(id);
            jira.username = value.username;
            jira.password = value.password;
            this._context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

