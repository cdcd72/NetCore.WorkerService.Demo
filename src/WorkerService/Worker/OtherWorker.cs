using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerService.Configuration.Core;

namespace WorkerService.Worker
{
    public class OtherWorker : BackgroundService
    {
        private readonly ILogger<OtherWorker> _logger;
        private readonly IOtherWorkerSettings _otherWorkerSettings;

        public OtherWorker(ILogger<OtherWorker> logger, IOtherWorkerSettings otherWorkerSettings)
        {
            _logger = logger;
            _otherWorkerSettings = otherWorkerSettings;
        }

        /// <summary>
        /// 覆寫 BackgroundService.StartAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("OtherWorker starting at: {time}", DateTimeOffset.Now);

            // 下行代碼為必要，因為還有繼承類需要處理的事情
            await base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// 覆寫 BackgroundService.ExecuteAsync
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("OtherWorker running at: {time}", DateTimeOffset.Now);

                // do your think...

                await Task.Delay(_otherWorkerSettings.OtherWorker.DelayTime, stoppingToken);
            }
        }

        /// <summary>
        /// 覆寫 BackgroundService.StopAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("OtherWorker stopping at: {time}", DateTimeOffset.Now);

            // 下行代碼為必要，因為還有繼承類需要處理的事情
            await base.StopAsync(cancellationToken);
        }

        #region Private Method

        #endregion
    }
}
