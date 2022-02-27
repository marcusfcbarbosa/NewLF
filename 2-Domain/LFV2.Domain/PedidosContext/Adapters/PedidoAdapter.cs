using LFV2.Domain.PedidosContext.Commands.Inputs;
using LFV2.Domain.PedidosContext.Entities;
using LFV2.Domain.PedidosContext.Models;

namespace LFV2.Domain.PedidosContext.Adapters
{
    //Pode tambem fazer uso do AutoMapper, fica a gosto do dev
    public static class PedidoAdapter
    {
        public static Pedido CommandToEntity(CriaPedidoCommand command)
        {
            return new Pedido(command.Nome,command.Obs);
        }
        public static PedidoModel EntityToModel(Pedido entitie)
        {
            return new PedidoModel
            {
                id = entitie.Id,
                Nome = entitie.Nome,
                Obs = entitie.Obs,
            };
        }
    }
}
