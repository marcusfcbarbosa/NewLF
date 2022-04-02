using System;
using System.Threading.Tasks;
using LFV2.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LFV2.Shared.TriggeringJobs
{
    //Funciona bacisamente como algum tipo de evento que é disparado
    public class TriggeringJob : ITriggeringJjob
    {
        private readonly ILogger<TriggeringJob> _logger;
        private readonly IServiceProvider _provider;
        public TriggeringJob(IServiceProvider provider
            , ILogger<TriggeringJob> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public void Trigger<T>(Action<T> bullet, Action<Exception> handler = null)
        {
            _logger.LogInformation("TriggeringJobs - Trigger a new action.");
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
                    _logger.LogError(e, "TriggeringJobs crashed!");
                    handler?.Invoke(e);
                }
                finally
                {
                    dependency = default;
                }
            });
        }

        public void TriggerAsync<T>(Func<T, Task> bullet, Action<Exception> handler = null)
        {
            _logger.LogInformation("TriggeringJobs - Fired a new action.");
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
                    _logger.LogError(e, "TriggeringJobs crashed!");
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
