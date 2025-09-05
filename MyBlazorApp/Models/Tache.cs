namespace MyBlazorApp.Models
{
    public class Tache
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public int Duree { get; set; }
        public Etat Etat { get; set; }
    }
}
