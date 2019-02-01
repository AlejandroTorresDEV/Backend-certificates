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
            try{
                Users UserResult = this._context.Users.Where(
                user => user.username.Equals(value.username)).FirstOrDefault();

                if(UserResult != null)
                {
                    if (UserResult.password == Encrypt.Hash(value.password))
                    {
                        string token = JWT.Encode(value.rolUser, "top secret", JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
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
