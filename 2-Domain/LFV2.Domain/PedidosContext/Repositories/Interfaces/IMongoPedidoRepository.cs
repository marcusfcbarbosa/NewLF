using System.Threading.Tasks;

namespace LFV2.Domain.PedidosContext.Repositories.Interfaces
{
    public interface IMongoPedidoRepository
    {
        public Task ReplicaParaBaseNoSql(int id, string nome, string obs);
    }
}
