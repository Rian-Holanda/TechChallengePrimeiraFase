using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumidor_Regioes_TechChallenge
{
    internal class WorkerCodigoAreaRegiao : BackgroundService
    {
        private readonly ILogger<WorkerCodigoAreaRegiao> _logger;

        public WorkerCodigoAreaRegiao(ILogger<WorkerCodigoAreaRegiao> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
