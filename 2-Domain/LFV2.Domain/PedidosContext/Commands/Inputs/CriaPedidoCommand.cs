using FluentValidator;
using FluentValidator.Validation;
using LFV2.Shared.Commands;
using MediatR;

namespace LFV2.Domain.PedidosContext.Commands.Inputs
{
    public class CriaPedidoCommand : Notifiable, ICommand, IRequest<ICommandResult>
    {
        public string Nome { get; set; }
        public string Obs { get; set; }
        public void Validate()
        {
            AddNotifications(new ValidationContract()
               .Requires()
               .IsNotNullOrEmpty(Nome, "Nome", "Nome é obrigatorio")
               );
            AddNotifications(new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(Obs, "Obs", "Obs é obrigatorio")
                );
        }
    }
}
