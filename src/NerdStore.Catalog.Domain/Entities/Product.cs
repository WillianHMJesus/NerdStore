using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string name, string description, decimal value, string image, int quantity, Dimension dimension)
        {
            Name = name;
            Description = description;
            Value = value;
            Image = image;
            Quantity = quantity;
            Dimension = dimension;
            Register = DateTime.Now;
            Active = true;

            Validate();
        }

        protected Product() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public string Image { get; private set; }
        public int Quantity { get; private set; }
        public DateTime Register { get; private set; }
        public bool Active { get; private set; }
        public Dimension Dimension { get; private set; }
        public Category Category { get; private set; }

        protected override void Validate()
        {
            AssertionConcern.ValidateNullOrEmpty(Name, "O campo Nome do produto não pode ser vazio.");
            AssertionConcern.ValidateNullOrEmpty(Description, "O campo Descrição do produto não pode ser vazio.");
            AssertionConcern.ValidateLessThanEqualMinimum(Value, 0, "O campo Valor do produto não pode ser menor ou igual a zero.");
            AssertionConcern.ValidateNullOrEmpty(Image, "O campo Imagem do produto não pode estar vazio.");
        }

        public void Enable() => Active = true;

        public void Disable() => Active = false;

        public void AssignCategory(Category category)
        {
            AssertionConcern.ValidateNull(category, "Não é possível atribuir uma categoria nula no produto.");
            Category = category;
        }

        public void DebitStock(int quantity)
        {
            AssertionConcern.ValidateLessThanEqualMinimum(quantity, 0, "A quantidade não pode ser menor que um para debitar o estoque.");
            AssertionConcern.ValidateLessThanEqualMinimum(Quantity, quantity, "O produto informado não possui estoque suficiente.");
            Quantity -= quantity;
        }

        public void ReplacingStock(int quantity)
        {
            AssertionConcern.ValidateLessThanEqualMinimum(quantity, 0, "A quantidade não pode ser menor que um para repor o estoque.");
            Quantity += quantity;
        }
    }
}
