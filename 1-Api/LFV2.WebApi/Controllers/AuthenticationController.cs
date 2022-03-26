using LFV2.Domain.PedidosContext.Commands.Inputs;
using LFV2.Domain.PedidosContext.Commands.Outputs;
using LFV2.Shared.Commands;
using LFV2.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LFV2.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ICommandResult> Post([FromBody] AuthenticationCommand command)
        {
            command.Validate();
            if (!command.Valid)
                return new CommandResult(false, "Erros", command.Notifications);

            if (command.UserName.ToLower() == "admin" && command.PassWord == "123456")
            {
                var token = TokenService.GenerateToken(command.UserName);
                return new CommandResult(true, "", new
                {
                    token = "Bearer " + token
                });
            }
            return new CommandResult(false, "404", StatusCodes.Status404NotFound);
        }
    }
}
