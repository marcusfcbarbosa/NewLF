using LFV2.Domain.PedidosContext.Repositories.Interfaces;
using LFV2.Infra.NoSQLContext.Documents;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace LFV2.Infra.NoSQLContext.Repositories
{
    public class MongoPedidoRepository : IMongoPedidoRepository
    {
        private readonly MongoContext _mongoContext;
        public MongoPedidoRepository(IOptions<ConfigDB> options)
        {
            _mongoContext = new MongoContext(options);
        }

        public async Task ReplicaParaBaseNoSql(int id, string nome, string obs)
        {
            var documentId = Guid.NewGuid();
            await _mongoContext.Pedidos.InsertOneAsync(new PedidoDocument(documentId, id, nome, obs));
        }
    }
}
