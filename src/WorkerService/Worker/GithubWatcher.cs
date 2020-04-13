using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WorkerService.Configuration.Core;

namespace WorkerService.Worker
{
    public class GithubWatcher : BackgroundService
    {
        private readonly ILogger<GithubWatcher> _logger;
        private readonly IGithubWatcherSettings _githubWatcherSettings;
        private readonly IHttpClientFactory _clientFactory;

        public GithubWatcher(ILogger<GithubWatcher> logger, IGithubWatcherSettings githubWatcherSettings, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _githubWatcherSettings = githubWatcherSettings;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// 覆寫 BackgroundService.StartAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GithubWatcher starting at: {time}", DateTimeOffset.Now);

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
                _logger.LogInformation("GithubWatcher running at: {time}", DateTimeOffset.Now);

                string userInfo = await GetUserInfo(_githubWatcherSettings.GithubWatcher.User);

                _logger.LogInformation(userInfo);

                await Task.Delay(_githubWatcherSettings.GithubWatcher.DelayTime, stoppingToken);
            }
        }

        /// <summary>
        /// 覆寫 BackgroundService.StopAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GithubWatcher stopping at: {time}", DateTimeOffset.Now);

            // 下行代碼為必要，因為還有繼承類需要處理的事情
            await base.StopAsync(cancellationToken);
        }

        #region Private Method

        /// <summary>
        /// 取得 Github 使用者資訊
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetUserInfo(string user)
        {
            var client = _clientFactory.CreateClient("Github");

            var response = await client.GetAsync(string.Format("users/{0}", user));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        #endregion
    }
}
