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
    public class UsersController : ControllerBase
    {

        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            this._context = context;
            if (this._context.Users.Count() == 1)
            {
                Console.WriteLine("No existen usuarios");
                Users usuario = new Users();
                //usuario.username = "Alejandro";
                //usuario.password = "pass2";
                this._context.Users.Add(usuario);
                this._context.SaveChanges();
            }
        }


        // GET api/users
        [HttpGet]
        public ActionResult<List<Users>> GetAll()
        {
            return this._context.Users.ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<Users> Get(long id)
        {
        
            Users user = this._context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users value)
        {
            this._context.Users.Add(value);
            this._context.SaveChanges();
            return value;
            //StatusCodeResult(200,value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] Users value)
        {
            Users user = this._context.Users.Find(id);
            user.username = value.username;
            user.password = value.password;
            this._context.SaveChanges();

        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public string Delete(long id)
        {
            Users userEliminar = this._context.Users.Where(user => user.username == "Alejandro").First();
            this._context.Remove(userEliminar);
            this._context.SaveChanges();
            return "Se ha borrado ->"+userEliminar.id;

        }

        private class UsersContext
        {
        }
    }
}