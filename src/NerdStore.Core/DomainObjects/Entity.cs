using System;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        protected abstract void Validate();

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(null, compareTo)) return false;
            if (ReferenceEquals(this, compareTo)) return true;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
    }
}
