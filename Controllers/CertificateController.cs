using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GttApiWeb.Models;
using GttApiWeb.Helpers;
using System.Web.Http;
using Jose;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using GttApiWeb.Helpers;

namespace GttApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CertificateController(AppDBContext context)
        {
            this._context = context;
        }

        // GET api/jira
        [HttpGet]
        public List<Certificates> Get()
        {
            return this._context.Certificates.ToList();
        }

        [HttpPost]
        public ActionResult<ResultError> Post([FromBody] Certificates value)
        {
            byte[] arrayBytes;
            X509Certificate2 x509;
            //Pasamos el string en bae64 para tranformarlo a un array de bytes.
            try
            {
                arrayBytes = System.Convert.FromBase64String(value.base64String);
                x509 = new X509Certificate2(arrayBytes, value.password);
                //Obtengo los elementos privados del certificado 
                value.numero_de_serie = x509.SerialNumber.ToString();
                value.subject = x509.Subject.ToString();
                value.entidad_emisiora = x509.Issuer.ToString();
                value.caducidad = x509.NotAfter;
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultError(400,"El formato del certificado es incorrecto.");
            }

            //Si el certificado es correcto lo guardamos
            try
            {
                value.password = Helpers.Encrypt.Hash(value.password);
                this._context.Certificates.Add(value);
                this._context.SaveChanges();
                return new ResultError(201, "Certificado guardado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultError(500, "Ha habido un error en el guardado.");

            }
        }

    }
}
