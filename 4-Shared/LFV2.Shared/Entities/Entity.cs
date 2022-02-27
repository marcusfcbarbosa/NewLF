using FluentValidator;
using LFV2.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFV2.Shared.Entities
{
    public class Entity : Notifiable, IEntity
    {
        public Entity()
        {
        }
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
