using LFV2.Domain.PedidosContext.Entities;
using LFV2.Domain.PedidosContext.Repositories.Interfaces;

namespace LFV2.Infra.SQLContext.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>,  IPedidoRepository
    {
        private readonly LFContext _context;
        public PedidoRepository(LFContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
