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
        [HttpGet("{id}")]
        public ActionResult<Certificates> Get(long id)
        {
            Certificates certificates = this._context.Certificates.Find(id);

            if (certificates == null)
            {
                return NotFound();
            }
            return certificates;
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


        /*
        * PUT api/certificate/id
        * Método para actualizar un certificado.       
        */
        [HttpPut("{id}")]
        public ActionResult<ResultError> Put(int id, [FromBody] Certificates value)
        {
            byte[] arrayBytes;
            X509Certificate2 x509;

            //Si el id es igual a q significa que tambien ha modficicado el certificado.
            if (id.Equals(1))
            {
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new ResultError(400, "El formato del certificado es incorrecto.");
                }
            }

            try
            {
                Certificates certificates = this._context.Certificates.Find(value.id);
                if (certificates != null)
                {
                    certificates.alias = value.alias;
                    certificates.base64String = value.base64String;
                    certificates.caducidad = value.caducidad;
                    certificates.nombre_cliente = value.nombre_cliente;
                    certificates.contacto_renovacion = value.contacto_renovacion;
                    certificates.eliminado = value.eliminado;
                    certificates.repositorio = value.repositorio;
                    certificates.observaciones = value.observaciones;
                    certificates.entidad_emisiora = value.entidad_emisiora;
                    certificates.numero_de_serie = value.numero_de_serie;
                    certificates.subject = value.subject;
                    certificates.id_orga = value.id_orga;
                    certificates.integration_list = value.integration_list;
                    certificates.nombreFichero = value.nombreFichero;
                    this._context.SaveChanges();
                    return new ResultError(201, "Certificado actualizado correctamente.");
                }
                return new ResultError(209, "No existe un certificado con ese ID.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResultError(500, "Ha ocurrido un error.");
            }
        }

        public void prueba()
        {
            Console.WriteLine("Prueba......");

            var today = DateTime.Now;
            Console.WriteLine(today);

            var todayMas1 = today.AddMonths(1);


            try{

                Certificates certificates = this._context.Certificates.Find(9);
                Console.WriteLine(certificates.alias);
            }


            catch (Exception E)
            {
                Console.WriteLine(E);

            }

            /* var certificate2 = _context.Certificates
                 .Where(certificate => certificate.caducidad > compareDate);*/

            /* Certificates certificate = (GttApiWeb.Models.Certificates)_context.Certificates
                 .Where(certi => certi.caducidad.AddMonths(1) <= todayMas1);*/

            //Console.WriteLine(certificate);
        }

    }
}
