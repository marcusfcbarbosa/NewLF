using LFV2.Infra.NoSQLContext.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LFV2.Infra.NoSQLContext
{
    public class MongoContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public MongoContext(IOptions<ConfigDB> options)
        {
            MongoClient mongoClient = new MongoClient(options.Value.ConnectionString);
            if (mongoClient != null)
            {
                _mongoDatabase = mongoClient.GetDatabase(options.Value.DataBase);
            }
        }
        public IMongoCollection<PedidoDocument> Pedidos
        {
            get
            {
                return _mongoDatabase.GetCollection<PedidoDocument>("Pedido");
            }
        }
    }
}
