using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Galeria
    {
        [Key]
        [Display(Name = "Código")]
        public int IdGaleria { get; set; }

        [Required]
        [Display(Name = "Título da Galeria")]
        public string Titulo { get; set; }

        public ICollection<Imagem?>? Imagens { get; set; }
    }
}
