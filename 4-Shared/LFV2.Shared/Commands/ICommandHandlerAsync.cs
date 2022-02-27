using System.Threading.Tasks;

namespace LFV2.Shared.Commands
{
    public interface ICommandHandlerAsync<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
