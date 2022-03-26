using LFV2.Domain.PedidosContext.Adapters;
using LFV2.Domain.PedidosContext.Commands.Inputs;
using LFV2.Domain.PedidosContext.Commands.Outputs;
using LFV2.Domain.PedidosContext.Entities;
using LFV2.Domain.PedidosContext.Repositories.Interfaces;
using LFV2.Shared.BackgroundTasks;
using LFV2.Shared.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LFV2.Domain.PedidosContext.CommandHandlers
{
    public class PedidoHandler : 
        IRequestHandler<CriaPedidoCommand, ICommandResult>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly BackgroundTask _backgroundTask;
        private readonly IMongoPedidoRepository _mongoPedidoRepository;
        public PedidoHandler(IPedidoRepository pedidoRepository,
            IMongoPedidoRepository mongoPedidoRepository,
            BackgroundTask backgroundTask)
        {
            _backgroundTask = backgroundTask;
            _pedidoRepository = pedidoRepository;
            _mongoPedidoRepository = mongoPedidoRepository;
        }

        public async Task<ICommandResult> Handle(CriaPedidoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var entity = PedidoAdapter.CommandToEntity(command);
                await _pedidoRepository.CreateAsync(entity);
                await _pedidoRepository.SaveChangesAsync();
                //Replica para a base NoSql, de maneira paralela
                _backgroundTask.FireAsync<IMongoPedidoRepository>(d => d.ReplicaParaBaseNoSql(entity.Id,entity.Nome,entity.Obs));
                return new CommandResult(true, "Pedido cadastrado com sucesso!", PedidoAdapter.EntityToModel(entity));
            }
            catch (Exception e)
            {
                return new CommandResult(false, $"Erro : {e.Message}", default(Pedido));
            }
        }
    }
}
