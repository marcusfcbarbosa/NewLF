namespace LFV2.Shared.Queries
{
    public interface IQueryHandler<T> where T : IQuerie
    {
        IQueryResult Handle(T command);
    }
}
