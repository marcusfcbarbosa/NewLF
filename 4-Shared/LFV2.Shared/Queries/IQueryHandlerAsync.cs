using System.Threading.Tasks;

namespace LFV2.Shared.Queries
{
    public interface IQueryHandlerAsync<T> where T : IQuerie
    {
        Task<IQueryResult> Handle(T querie);
    }
}
