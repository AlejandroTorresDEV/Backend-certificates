using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GttApiWeb.Models;
using GttApiWeb.Helpers;

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

        /*
         * GET api/api/id
         * Método para buscar un usuario por ID
         */        
        [HttpGet("{id}")]
        public ActionResult<Jira> Get(long id)
        {
            try
            {
                Jira jiraExistente = this._context.Jira.Where(
                 jira => jira.user_id == id).FirstOrDefault();

                if (jiraExistente != null)
                {
                    return jiraExistente;
                }
                return NotFound();
            }
            catch(Exception e)
            {
                return Unauthorized();
            }
           
        }

        /*
         * POST api/jira/
         * Método para agregar una cuenta unica de Jira a un usuario.        
         */
        [HttpPost]
        public ActionResult<ResultError> Post([FromBody] Jira value)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine(value);
           Jira userExistencia = this._context.Jira.Where(
                        jira => jira.user_id == value.user_id).FirstOrDefault();

            if(userExistencia == null)
            {
                value.password = Encrypt.Hash(value.password);
                this._context.Jira.Add(value);
                this._context.SaveChanges();
                return new ResultError("error", 200, "Cuenta de Jira creada correctamente.",null,null);
            }
            return new ResultError("error", 209, "El usuario ya tiene una cuenta de Jira asociada.", null, null);
        }


        /*
        * PUT api/jira/id
        * Método para actualizar una cuenta de Jira.       
        */
        [HttpPut("{id}")]
        public ActionResult<ResultError> Put(long id, [FromBody] Jira value)
        {
            Jira jiraExistente = this._context.Jira.Where(
                                   jira => jira.user_id == id).FirstOrDefault();

            if (jiraExistente != null)
            {
                value.password = Encrypt.Hash(value.password);
             
                this._context.SaveChanges();
                return new ResultError("error", 200, "Cuenta de Jira actualizada correctamente.", null, null);
            }
            return new ResultError("error", 209, "No existe una cuenta de Jira con esa ID.", null, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

