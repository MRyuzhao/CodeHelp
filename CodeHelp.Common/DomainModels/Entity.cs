using System;

namespace CodeHelp.Common.DomainModels
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get;}
    }
}