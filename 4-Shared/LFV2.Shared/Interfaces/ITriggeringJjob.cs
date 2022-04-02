using System;
using System.Threading.Tasks;

namespace LFV2.Shared.Interfaces
{
    public interface ITriggeringJjob
    {
        public void Trigger<T>(Action<T> bullet, Action<Exception> handler = null);

        public void TriggerAsync<T>(Func<T, Task> bullet, Action<Exception> handler = null);
    }
}
