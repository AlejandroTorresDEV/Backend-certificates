using System;
using System.Text;
using System.Security.Cryptography;

namespace GttApiWeb.Helpers
{
    public class ResultError
    {
        public string error { get; set; }
        public int statusCode { get; set; }
        public string mensaje { get; set; }
        public string id { get; set; }
        public string jwt { get; set; }

        public ResultError(String error,int statusCode,String mensaje)
        {
            this.error = error;
            this.statusCode = statusCode;
            this.mensaje = mensaje;
        }

        public ResultError(String error, int statusCode, String mensaje,String jwt,String id)
        {
            this.error = error;
            this.statusCode = statusCode;
            this.mensaje = mensaje;
            this.jwt = jwt;
            this.id = id;
        }
    }
}

