using System;
using System.Text;
using System.Security.Cryptography;

namespace GttApiWeb.Helpers
{
    public class ResultError
    {
        public int statusCode { get; set; }
        public string mensaje { get; set; }
        public string id { get; set; }
        public string jwt { get; set; }
        public string rolUser { get; set;}

        public ResultError(int statusCode,String mensaje)
        {
            this.statusCode = statusCode;
            this.mensaje = mensaje;
        }

        public ResultError(int statusCode, String mensaje,String jwt,String id,String rolUser)
        {
            this.statusCode = statusCode;
            this.mensaje = mensaje;
            this.jwt = jwt;
            this.id = id;
            this.rolUser = rolUser;
        }
    }
}

