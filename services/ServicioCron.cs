using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GttApiWeb.Models;
using System.Linq;
using GttApiWeb.Controllers;

namespace GttApiWeb.services
{
    internal class ServicioCron : IHostedService,IDisposable
    {
        private AppDBContext _context;
        private readonly ILogger _logger;
        private Timer _timer;

        public ServicioCron(ILogger<ServicioCron> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Arrancando el servicio");
            _timer = new Timer(DoWork,null, TimeSpan.Zero, TimeSpan.FromDays(5));

            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {

            _logger.LogInformation("Iniciando el servicio");
            CertificateController certificateController = new CertificateController(_context);
            //.prueba();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Parando el servicio");
            //Hasta que le digamos que para sigue hasta el infinito.
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
       
    }
}
