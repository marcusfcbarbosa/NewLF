using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LFV2.Shared.BackgroundTasks
{
    //Funciona bacisamente como algum tipo de evento que é disparado
    public class BackgroundTask
    {
        private readonly ILogger<BackgroundTask> _logger;
        private readonly IServiceProvider _provider;

        public BackgroundTask(IServiceProvider provider
            , ILogger<BackgroundTask> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public void Fire<T>(Action<T> bullet, Action<Exception> handler = null)
        {
            _logger.LogInformation("BackgroundTask - Fired a new action.");
            Task.Run(() =>
            {
                using var scope = _provider.CreateScope();
                var dependency = scope.ServiceProvider.GetRequiredService<T>();
                try
                {
                    bullet(dependency);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "BackgroundTask crashed!");
                    handler?.Invoke(e);
                }
                finally
                {
                    dependency = default;
                }
            });
        }

        public void FireAsync<T>(Func<T, Task> bullet, Action<Exception> handler = null)
        {
            _logger.LogInformation("BackgroundTask - Fired a new action.");
            Task.Run(async () =>
            {
                using var scope = _provider.CreateScope();
                var dependency = scope.ServiceProvider.GetRequiredService<T>();
                try
                {
                    await bullet(dependency);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "BackgroundTask crashed!");
                    handler?.Invoke(e);
                }
                finally
                {
                    dependency = default;
                }
            });
        }

    }
}
