﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GttApiWeb.Models;
using GttApiWeb.Helpers;

namespace ApiGTT.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase{
        private readonly AppDBContext _context;

        public UsersController(AppDBContext context){
            this._context = context;
            //Creamos usuarios iniciales al iniciar el controlador.
            if (this._context.Users.Count() == 0){
                Users usuario = new Users();
                usuario.username = "Administrador";
                usuario.rolUser = RolUser.admin;
                usuario.password = Encrypt.Hash("12345678");
                this._context.Users.Add(usuario);
                this._context.SaveChanges();
            }
        }

        // GET api/users
        [HttpGet]
        public ActionResult<List<Users>> GetAll(){
            return this._context.Users.ToList();
        }

        // GET api/users/id
        [HttpGet("{id}")]
        public ActionResult<Users> Get(long id){
            Users user = this._context.Users.Find(id);

            if (user == null){
                return NotFound();
            }
            return user;
        }

        /*
         * POST api/users/
         * Método para agregar nuevos usuarios.        
         */
        [HttpPost]
        public ActionResult<ResultError> Post([FromBody] Users value){

            try{
                var headerValue = Request.Headers["Authorization"];
                var token = Jose.JWT.Decode(headerValue,UtilsTokens.secretKey);
                try{
                    //Comprobamos si existe el usuario
                    Users userExistencia = this._context.Users.Where(
                                user => user.username == value.username).FirstOrDefault();

                    if (userExistencia == null){
                        value.password = Encrypt.Hash(value.password);
                        this._context.Users.Add(value);
                        this._context.SaveChanges();
                        return new ResultError(200, "Usuario creado correctamente.");

                    }
                    return new ResultError(209, "El nombre del usuario ya existe.");
                }catch (Exception e){
                    Console.WriteLine(e);
                    return new ResultError(500, "Ha ocurrido un error.");
                }
            }catch (Exception e){
                Console.WriteLine(e);
                return Unauthorized();
            }
        }


        /*
         * ENDPOINTS no utilizados
         *         
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] Users value){
            Users user = this._context.Users.Find(id);
            user.username = value.username;
            user.password = value.password;
            this._context.SaveChanges();
        }


        [HttpDelete("{id}")]
        public ActionResult<string> Delete(long id){

            Users userEliminar = this._context.Users.Where(
                user => user.id == id).First();

            if (userEliminar == null){
                return "No existe usuario";
            }

            this._context.Remove(userEliminar);
            this._context.SaveChanges();
            return "Se ha borrado ->" + userEliminar.id;
        }*/
    }
}