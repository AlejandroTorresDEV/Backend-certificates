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
        public ActionResult<String> Get()
        {
            return "hola";
        }

        public ActionResult<string>  Post( string value)
        {


            X509Certificate2 certificate = new X509Certificate2("/Users/alejandrotorresruiz/Desktop/certificados/prueba.pfx","1234");

            string expirationDate = certificate.GetExpirationDateString();
            string issuer = certificate.Issuer;
            string effectiveDateString = certificate.GetEffectiveDateString();
            string nameInfo = certificate.GetNameInfo(X509NameType.SimpleName, true);
            bool hasPrivateKey = certificate.HasPrivateKey;
            var serialNumber = certificate.GetSerialNumber();
            Console.WriteLine("1."+expirationDate);
            Console.WriteLine("2." + issuer);
            Console.WriteLine("3." + effectiveDateString);
            Console.WriteLine("4." + nameInfo);
            Console.WriteLine("5." + hasPrivateKey);
            Console.WriteLine("6." + serialNumber);

            //Console.WriteLine(value);
            return "hola";
        }

    }
}
