using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GttApiWeb.Models;
using GttApiWeb.Helpers;
using Jose;
namespace GttApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        Array secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

        public AuthController(AppDBContext contex)
        {
            this._context = contex;
        }

        private readonly AppDBContext _context;
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/auth
        [HttpPost]
        public ActionResult<ResultError> Post([FromBody] Users value){

            try
            {
                Users UserResult = this._context.Users.Where(
                user => user.username.Equals(value.username)).FirstOrDefault();

                if(UserResult != null)
                {
                    if (UserResult.password == Encrypt.Hash(value.password))
                    {
                        var payload = new Dictionary<string, object>()
                        {
                            { "rol", UserResult.rolUser },
                            { "name", UserResult.username }
                        };
                        string token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);

                        return new ResultError("error", 200, "Login realizado correctamente.", token, "" + UserResult.rolUser);
                    }
                }
                return new ResultError("error", 204, "Las credenciales son incorrectas.");
            }
            catch (Exception e){
                Console.WriteLine(e);
                return NotFound();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
