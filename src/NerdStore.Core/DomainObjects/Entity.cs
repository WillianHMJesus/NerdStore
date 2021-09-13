using FluentValidation.Results;
using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        private List<Event> _events;
        public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void AddEvent(Event _event)
        {
            _events = _events ?? new List<Event>();
            _events.Add(_event);
        }

        public void RemoveEvent(Event _event)
        {
            _events?.Remove(_event);
        }

        public void ClearEvents()
        {
            _events?.Clear();
        }

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
