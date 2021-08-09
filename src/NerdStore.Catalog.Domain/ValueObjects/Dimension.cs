using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.ValueObjects
{
    public class Dimension
    {
        public Dimension(decimal height, decimal width, decimal? depth = null)
        {
            Height = height;
            Width = width;
            Depth = depth;

            Validate();
        }

        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal? Depth { get; private set; }

        public void Validate()
        {
            AssertionConcern.ValidateLessThanEqualMinimum(Height, 0, "O campo Altura da dimensão não pode ser menor ou igual a zero.");
            AssertionConcern.ValidateLessThanEqualMinimum(Width, 0, "O campo Largura da dimensão não pode ser menor ou igual a zero.");
            if (Depth.HasValue)
                AssertionConcern.ValidateLessThanEqualMinimum(Depth.Value, 0, "O campo Profundidade da dimensão não pode ser menor ou igual a zero.");
        }

        public override string ToString()
        {
            if(Depth == null)
                return $"LxA: {Width} x {Height}";

            return $"LxAxP: {Width} x {Height} x {Depth}";
        }
    }
}
