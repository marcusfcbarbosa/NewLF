using System;

namespace LFV2.Shared.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime CreateAt { get; set; }
        DateTime? UpdateAt { get; set; }

    }
}
