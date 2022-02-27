using LFV2.Shared.Entities;
using System;

namespace LFV2.Domain.PedidosContext.Entities
{
    public class Pedido : Entity
    {
        public string Nome { get; private set; }
        public string Obs { get; private set; }
        private Pedido(){}
        public Pedido(string nome, string obs) 
            : this()
        {
            Nome = nome;
            Obs = obs;
            CreateAt = DateTime.Now;
        }

        public void AtualizaPedido(string nome, string obs) {

            Nome = nome;
            Obs = obs;
            UpdateAt = DateTime.Now;
        }
       

        
    }
}
