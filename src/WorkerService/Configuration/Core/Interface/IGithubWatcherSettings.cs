namespace WorkerService.Configuration.Core
{
    public interface IGithubWatcherSettings
    {
        GithubWatcherOption GithubWatcher { get; }
    }
}
