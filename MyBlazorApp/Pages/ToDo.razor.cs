using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyBlazorApp.Components;
using MyBlazorApp.Models;
using System.Threading.Tasks;

namespace MyBlazorApp.Pages
{
    public partial class ToDo
    {
        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        public List<Tache> Taches = [
                new Tache { Nom = "Chercher mes lunettes perdues dans le noir", Duree = 10, Etat = Etat.Pending },
            new Tache { Nom = "Boire un café qui m'éveille jusqu'au soir", Duree = 15, Etat = Etat.Pending },
            new Tache { Nom = "Discuter du temps comme on discute du vin", Duree = 20, Etat = Etat.Pending },
            new Tache { Nom = "Envoyer des emails pour éviter le destin", Duree = 25, Etat = Etat.Pending },
            new Tache { Nom = "Tester la chaise, mon postérieur s'en souvient", Duree = 10, Etat = Etat.Pending },
            new Tache { Nom = "Réécrire le code que je hais mais qui m'enchaîne", Duree = 45, Etat = Etat.Pending },
            new Tache { Nom = "Faire semblant de coder tout en rêvant de Crème", Duree = 30, Etat = Etat.Pending },
            new Tache { Nom = "Ranger le bureau, rien ne sert mais on s'y traîne", Duree = 50, Etat = Etat.Pending },
            new Tache { Nom = "Inventer des noms qui font rire le programme", Duree = 20, Etat = Etat.Pending },
            new Tache { Nom = "Chercher le câble que l'on croit être une flamme", Duree = 25, Etat = Etat.Pending },
            new Tache { Nom = "Planifier la pause où l'on boira des sodas", Duree = 10, Etat = Etat.Pending },
            new Tache { Nom = "Faire une sieste au bureau, noble combat", Duree = 30, Etat = Etat.Pending },
            new Tache { Nom = "Écrire un rapport que personne ne lira", Duree = 40, Etat = Etat.Pending },
            new Tache { Nom = "Réfléchir à des bugs qui jamais ne se voient", Duree = 35, Etat = Etat.Pending },
            new Tache { Nom = "Rêver à des lignes que le compiler noie", Duree = 20, Etat = Etat.Pending },
            new Tache { Nom = "Envoyer des messages pour se croire occupé", Duree = 15, Etat = Etat.Pending },
            new Tache { Nom = "Chercher les snacks cachés par l’équipe affamée", Duree = 25, Etat = Etat.Pending },
            new Tache { Nom = "Se perdre dans le code comme en forêt enchantée", Duree = 45, Etat = Etat.Pending },
            new Tache { Nom = "Faire semblant de taper pour paraître inspiré", Duree = 20, Etat = Etat.Pending },
            new Tache { Nom = "Méditer sur l’éternelle réunion terminée", Duree = 15, Etat = Etat.Pending }
            ];

        public void ChangeState(Tache t)
        {
            t.Etat = (Etat)((((int)t.Etat) + 1) % Enum.GetValues<Etat>().Count());
        }

        public async void RemoveTask(Tache t)
        {
            // ouvrir la boite de dialog
            var dialogRef = await DialogService.ShowAsync<ConfirmDialog>();

            var result = await dialogRef.Result;

            if (!result!.Canceled)
            {
                Taches.Remove(t);
                StateHasChanged();
            }

        }

        public async Task OpenAddDialog()
        {
            var dialogRef = await DialogService.ShowAsync<AddTask>();
            var result = await dialogRef.Result;
            if(result != null)
            {
                if(result?.Data is Tache t)
                {
                    Taches.Add(t);
                }
            }
        }
    }
}
