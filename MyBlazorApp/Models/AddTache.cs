using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.Models
{
    public class AddTache
    {
        [Required(ErrorMessage = "Ce Champs est requis")]
        [MinLength(2, ErrorMessage = "Doit faire au - 2 caractères")]
        public string Nom { get; set; } = null!;
        [Range(1, int.MaxValue, ErrorMessage = "Au moins 1")]
        public int Duree { get; set; } // minutes
        public Etat Etat { get; set; }
    }
}
