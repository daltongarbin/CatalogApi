using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels.ProductViewModels
{
    public class EditorProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [DataType(DataType.Text, ErrorMessage = "Titulo invalido")]
        public string Title { get; set; }
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}