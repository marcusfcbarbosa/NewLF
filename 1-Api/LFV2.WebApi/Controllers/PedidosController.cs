using LFV2.Domain.PedidosContext.Commands.Inputs;
using LFV2.Domain.PedidosContext.Commands.Outputs;
using LFV2.Shared.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LFV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<ICommandResult> Post([FromServices] IMediator mediator,
                                                [FromBody] CriaPedidoCommand command)
        {
            command.Validate();
            if (command.Valid)
                return await mediator.Send(command);
            return new CommandResult(false, "Erros", command.Notifications);
        }
    }
}
