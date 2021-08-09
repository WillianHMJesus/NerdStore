using NerdStore.Core.DomainObjects;
using System.Collections.Generic;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Category : Entity
    {
        public Category(int code, string name, string description = null)
        {
            Code = code;
            Name = name;
            Description = description;

            Validate();
        }

        protected Category() { }

        public int Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<Product> Products { get; set; }

        protected override void Validate()
        {
            AssertionConcern.ValidateLessThanEqualMinimum(Code, 0, "O campo Código da categoria não pode ser menor ou igual a zero.");
            AssertionConcern.ValidateNullOrEmpty(Name, "O campo Nome da categoria não pode ser vazio.");
        }
    }
}
