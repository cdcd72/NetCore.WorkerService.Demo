namespace WorkerService.Configuration.Core
{
    /// <summary>
    /// 經解析後的組態資料介面
    /// </summary>
    public interface IWorkerSettingsResolved : IGithubWatcherSettings, IOtherWorkerSettings 
    { 

    }
}
