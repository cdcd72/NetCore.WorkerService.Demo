using WorkerService.Configuration.Core;
using Microsoft.Extensions.Options;
using System;

namespace WorkerService.Configuration.Intermediaries
{
    /// <summary>
    /// 橋接 WorkerSettings
    /// </summary>
    public class WorkerSettingsBridge : IWorkerSettingsResolved
    {
        private WorkerSettings _workerSettings;

        public WorkerSettingsBridge(IOptionsMonitor<WorkerSettings> workerSettings)
        {
            _workerSettings = workerSettings.CurrentValue ?? throw new ArgumentNullException(nameof(workerSettings));

            // 當設定組態改變時觸發
            workerSettings.OnChange(OnSettingsChanged);
        }


        public GithubWatcherOption GithubWatcher => _workerSettings.GithubWatcher;

        public OtherWorkerOption OtherWorker => _workerSettings.OtherWorker;

        #region Private Method

        /// <summary>
        /// 當設定組態改變時要做的事情
        /// </summary>
        /// <param name="workerSettings"></param>
        private void OnSettingsChanged(WorkerSettings workerSettings)
        {
            _workerSettings = workerSettings;
        }

        #endregion
    }
}
