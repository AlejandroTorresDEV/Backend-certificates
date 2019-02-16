using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GttApiWeb.Models;
using GttApiWeb.Helpers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace GttApiWeb.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class JiraController : ControllerBase{
        private readonly AppDBContext _context;

        public JiraController(AppDBContext context){
            this._context = context;
        }

        // GET api/jira
        [HttpGet]
        public ActionResult<List<Jira>> Get(){
            return this._context.Jira.ToList();
        }

        /*
         * GET api/api/id
         * Método para buscar un usuario por ID
         */
        [HttpGet("{id}")]
        public ActionResult<Jira> Get(long id){
            try{
                var headerValue = Request.Headers["Authorization"];
                var token = Jose.JWT.Decode(headerValue, Helpers.UtilsTokens.secretKey);
                Jira jiraExistente = this._context.Jira.Where(
                jira => jira.user_id == id).FirstOrDefault();

                if (jiraExistente != null){
                    return jiraExistente;
                }
                return NotFound();
            }
            catch (Exception e) {
            Console.WriteLine(e);
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
            try{
                var headerValue = Request.Headers["Authorization"];
                var token = Jose.JWT.Decode(headerValue, Helpers.UtilsTokens.secretKey);

                try{
                    Jira userExistencia = this._context.Jira.Where(
                            jira => jira.user_id == value.user_id).FirstOrDefault();

                    if (userExistencia == null){
                        value.password = Encrypt.Hash(value.password);
                        value.issue = JiraElementosUtils.issue;
                        this._context.Jira.Add(value);
                        this._context.SaveChanges();
                        return new ResultError(200, "Cuenta de Jira creada correctamente.");
                    }
                    return new ResultError(209, "El usuario ya tiene una cuenta de Jira asociada.");
                }catch (Exception e){
                    Console.WriteLine(e);
                    return new ResultError(500, "Ha ocurrido un error.");
                }
            }
            catch (Exception e){
                Console.WriteLine(e);
                return Unauthorized();
            }
        }

        /*
        * PUT api/jira/id
        * Método para actualizar una cuenta de Jira.       
        */
        [HttpPut("{id}")]
        public ActionResult<ResultError> Put(long id, [FromBody] Jira value){
            try{
                var headerValue = Request.Headers["Authorization"];
                var token = Jose.JWT.Decode(headerValue, Helpers.UtilsTokens.secretKey);
                try{
                    Jira jira = this._context.Jira.Find(id);

                    if (jira != null){
                        jira.username = value.username;
                        jira.password = Encrypt.Hash(value.password);
                        jira.url = value.url;
                        jira.proyect = value.proyect;
                        jira.componente = value.componente;
                        this._context.SaveChanges();
                        return new ResultError(200, "Cuenta de Jira actualizada correctamente.");
                    }
                    return new ResultError(209, "No existe una cuenta de Jira asociada a esa ID.");

                }catch (Exception e){
                    Console.WriteLine(e);
                    return new ResultError(500, "Ha ocurrido un error.");
                }

            }catch (Exception e){
                Console.WriteLine(e);
                return Unauthorized();
            }
        }
    }
}


