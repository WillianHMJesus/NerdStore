using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Código")]
        public int? Code { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }
    }
}
