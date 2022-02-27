using LFV2.Domain.PedidosContext.Commands.Inputs;
using LFV2.Shared.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LFV2.Domain.PedidosContext.CommandHandlers
{
    public class PedidoHandler : IRequestHandler<CriaPedidoCommand, ICommandResult>
    {
        public Task<ICommandResult> Handle(CriaPedidoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
