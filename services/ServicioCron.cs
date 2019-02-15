using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GttApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GttApiWeb.services
{
    internal class ServicioCron : IHostedService,IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;

        public ServicioCron(ILogger<ServicioCron> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
        
            _logger.LogInformation("Arrancando el servicio");
            _timer = new Timer(DoWork,null, TimeSpan.Zero, TimeSpan.FromHours(12));


                return Task.CompletedTask;
        }

        public void DoWork(object state)
        {
            _logger.LogInformation("Iniciando el servicio");

            var optionsBuild = new DbContextOptionsBuilder<AppDBContext>();


            //optionsBuild.UseNpgsql("Host=192.168.99.101; Port=5432;Username=postgres;Password=example;Database=ApiGtt;");
            optionsBuild.UseNpgsql("Host=192.168.99.101; Port=5432;Username=postgres;Password=example;Database=ApiGtt;");
            using (var context = new AppDBContext(optionsBuild.Options))
            {
                context.Certificates.Load();

                var today = DateTime.Now;

                var fecha_actual_mas_mes = today.AddMonths(1);

                foreach (var certificates in context.Certificates.Local)
                {
                    if (certificates.caducidad <= fecha_actual_mas_mes)
                    {
                        _logger.LogInformation("CADUCIDAD" + certificates.caducidad);
                        if (certificates.caducidad < today)
                        {
                            _logger.LogInformation("fecha actual"+ fecha_actual_mas_mes);
                            _logger.LogInformation("MAS DE UN MES CADUCADOS");
                            _logger.LogInformation("" + certificates.caducidad);
                            certificates.estado = Estado.caducado;
                            certificates.eliminado = true;
                            context.SaveChanges();

                        }else 
                        if (certificates.caducidad > today)
                        {
                            _logger.LogInformation("PROXIMO A CADUCAR");
                            _logger.LogInformation("" + certificates.caducidad);
                            certificates.estado = Estado.proxima;
                            context.SaveChanges();

                        }

                    }
                }
            }
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
