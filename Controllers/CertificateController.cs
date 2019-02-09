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

        [HttpPost]
        public ActionResult<Certificates> Post([FromBody] Certificates value)
        {

            byte[] arrayBytes = System.Convert.FromBase64String(value.base64String);

             X509Certificate2 x509 = new X509Certificate2(arrayBytes, "111111");

             string token = x509.ToString(true);

             //Obtengo los elementos privados del certificado 
             value.numero_de_serie = x509.SerialNumber.ToString();
             value.subject = x509.Subject.ToString();
             value.entidad_emisiora = x509.Issuer.ToString();
             value.caducidad = x509.NotAfter;

             this._context.Certificates.Add(value);
             this._context.SaveChanges();
             // Por ahora solo devuelve todos los datos
             //return new ErrorApi(200, token);
             //X509Certificate2 x509 = new
             //X509Certificate2(value, "1234");
             // X509Certificate2 x509 = new X509Certificate2(, "1234");
             //Create X509Certificate2 object from .cer file.
             //byte[] rawData = ReadFile();

             /*X509Certificate2 x509 = new X509Certificate2();
              //x509.Import(value);

             //Print to console information contained in the certificate.
             Console.WriteLine("{0}Subject: {1}{0}", Environment.NewLine, x509.Subject);
             Console.WriteLine("{0}Issuer: {1}{0}", Environment.NewLine, x509.Issuer);
             Console.WriteLine("{0}Version: {1}{0}", Environment.NewLine, x509.Version);
             Console.WriteLine("{0}Valid Date: {1}{0}", Environment.NewLine, x509.NotBefore);
             Console.WriteLine("{0}Expiry Date: {1}{0}", Environment.NewLine, x509.NotAfter);
             Console.WriteLine("{0}Thumbprint: {1}{0}", Environment.NewLine, x509.Thumbprint);
             Console.WriteLine("{0}Serial Number: {1}{0}", Environment.NewLine, x509.SerialNumber);
             Console.WriteLine("{0}Friendly Name: {1}{0}", Environment.NewLine, x509.PublicKey.Oid.FriendlyName);
             Console.WriteLine("{0}Public Key Format: {1}{0}", Environment.NewLine, x509.PublicKey.EncodedKeyValue.Format(true));
             //Console.WriteLine("{0}Raw Data Length: {1}{0}", Environment.NewLine, x509.RawData.Length);
             //Console.WriteLine("{0}Certificate to string: {1}{0}", Environment.NewLine, x509.ToString(true));

             //Console.WriteLine("{0}Certificate to XML String: {1}{0}", Environment.NewLine, x509.PublicKey.Key.ToXmlString(false));

             var num =  (Environment.NewLine, x509.SerialNumber);

             Console.WriteLine("-------------------" + num);
             /*
             //Add the certificate to a X509Store.
             X509Store store = new X509Store();
             store.Open(OpenFlags.MaxAllowed);
             store.Add(x509);
             store.Close();*/

            //String byteCertificado = 

            /* X509Certificate2 certificate = new X509Certificate2("/Users/alejandrotorresruiz/Desktop/certificados/prueba.pfx","1234");

             string expirationDate = certificate.GetExpirationDateString();
             string issuer = certificate.Issuer;
             string effectiveDateString = certificate.GetEffectiveDateString();
             string nameInfo = certificate.GetNameInfo(X509NameType.SimpleName, true);
             bool hasPrivateKey = certificate.HasPrivateKey;
             Byte[] serialNumber = certificate.GetSerialNumber();
             Console.WriteLine("1."+expirationDate);
             Console.WriteLine("2." + issuer);
             Console.WriteLine("3." + effectiveDateString);
             Console.WriteLine("4." + nameInfo);
             Console.WriteLine("5." + hasPrivateKey);
             Console.WriteLine("6." + serialNumber);*/


            //Console.WriteLine(value);

            Console.WriteLine("{0}Subject: {1}{0}", Environment.NewLine, x509.Subject);
             Console.WriteLine("{0}Issuer: {1}{0}", Environment.NewLine, x509.Issuer);
             Console.WriteLine("{0}Version: {1}{0}", Environment.NewLine, x509.Version);
             Console.WriteLine("{0}Valid Date: {1}{0}", Environment.NewLine, x509.NotBefore);
             Console.WriteLine("{0}Expiry Date: {1}{0}", Environment.NewLine, x509.NotAfter);
             Console.WriteLine("{0}Thumbprint: {1}{0}", Environment.NewLine, x509.Thumbprint);
             Console.WriteLine("{0}Serial Number: {1}{0}", Environment.NewLine, x509.SerialNumber);
             Console.WriteLine("{0}Friendly Name: {1}{0}", Environment.NewLine, x509.PublicKey.Oid.FriendlyName);
             Console.WriteLine("{0}Public Key Format: {1}{0}", Environment.NewLine, x509.PublicKey.EncodedKeyValue.Format(true));
            return value;
        }

    }
}
