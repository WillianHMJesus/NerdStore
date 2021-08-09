using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Valor")]
        public decimal? Value { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Imagem")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Quantidade")]
        public int? Quantity { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Altura")]
        public decimal? Height { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Largura")]
        public decimal? Width { get; set; }

        [DisplayName("Profundidade")]
        public decimal? Depth { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Código da Categoria")]
        public int? CategoryCode { get; set; }
    }
}
