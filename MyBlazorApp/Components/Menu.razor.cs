namespace MyBlazorApp.Components
{
    public partial class Menu
    {

        public List<MenuItem> MenuItems => [
            new MenuItem { Title = "Accueil", Href = "/" },
            new MenuItem { Title = "À Propos", Href = "/about" },
            new MenuItem { Title = "Liste des tâches", Href = "/todo" },
        ];

        public class MenuItem
        {
            public string Href { get; set; } = null!;
            public string Title { get; set; } = null!;
        }

    }
}
