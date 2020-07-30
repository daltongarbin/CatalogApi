using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column(TypeName = "varchar(60)")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Title { get; set; }


        [MaxLength(1024, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres")]
        [Column(TypeName = "varchar(1024)")]
        public string Description { get; set; }


        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column(TypeName = "varchar(1024)")]
        public string Image { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime CreateDate { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime LastUpdateDate { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}