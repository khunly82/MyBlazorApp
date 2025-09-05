using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyBlazorApp.Components
{
    public partial class Menu
    {
        [Inject]
        public AuthenticationStateProvider AuthState { get; set; } = null!;

        public List<MenuItem> MenuItems => [
            new MenuItem { Title = "Accueil", Href = "/" },
            new MenuItem { Title = "À Propos", Href = "/about" },
            new MenuItem { Title = "Liste des tâches", Href = "/todo" },
        ];

        public void Logout()
        {
            ((MyAuthStateProvider)AuthState).Token = null;
        }

        public class MenuItem
        {
            public string Href { get; set; } = null!;
            public string Title { get; set; } = null!;
        }

    }
}
