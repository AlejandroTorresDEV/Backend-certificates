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

        private readonly AppDBContext _context;

        public AuthController(AppDBContext contex)
        {
            this._context = contex;
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
                            { "name", UserResult.username },
                            { "id" , UserResult.id}
                        };
                        string token = Jose.JWT.Encode(payload, UtilsTokens.secretKey, JwsAlgorithm.HS256);

                        return new ResultError( 200, "Login realizado correctamente.", token, "" + UserResult.id,UserResult.rolUser.ToString());
                    }
                }
                return new ResultError(204, "Las credenciales son incorrectas.");
            }
            catch (Exception e){
                Console.WriteLine(e);
                return NotFound();
            }
        }
    }
}
