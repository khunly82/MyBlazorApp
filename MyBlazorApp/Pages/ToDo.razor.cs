using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyBlazorApp.Components;
using MyBlazorApp.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MyBlazorApp.Pages
{
    [Authorize]
    public partial class ToDo
    {
        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        // afficher des messages pour l'ultilisateur
        [Inject]
        public ISnackbar SnackBar { get; set; } = null!;

        [Inject]
        public HttpClient Http { get; set; } = null!;

        public List<Tache> Taches = [];

        protected async override Task OnInitializedAsync()
        {
            Taches = await Http.GetFromJsonAsync<List<Tache>>("taches")
                ?? throw new HttpRequestException();
        }

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
                // suppression de l'api
                var response = await Http.DeleteAsync($"taches/{t.Id}");
                // si la suppression c'est bien passée
                if(response.IsSuccessStatusCode)
                {
                    Taches.Remove(t);
                }
                else
                {
                    if(response.StatusCode == HttpStatusCode.NotFound)
                    {
                        SnackBar.Add("Cet element a déjà été supprimé", Severity.Error);
                        // dejà supprimé
                    }
                    else
                    {
                        // le serveur ne peut pas supprimé votre tache
                        SnackBar.Add(response.ReasonPhrase!, Severity.Error);
                    }
                    // mettre à jour la liste locale
                    Taches = await Http.GetFromJsonAsync<List<Tache>>("taches")
                        ?? throw new HttpRequestException();
                }
                // mettre à jour l'api
                StateHasChanged();
            }
        }

        public async Task OpenAddDialog()
        {
            var dialogRef = await DialogService.ShowAsync<AddTask>();
            var result = await dialogRef.Result;
            if(result != null)
            {
                if(result?.Data is AddTache t)
                {
                    var response = await Http.PostAsJsonAsync("taches", new {
                        t.Nom,
                        t.Duree,
                        Etat = Etat.Pending
                    });

                    //// recupérer la reponse pour obtenir l'id de la nouvelle
                    //Tache newt = JsonConvert.DeserializeObject<Tache>(await response.Content.ReadAsStringAsync())!;

                    if(response.IsSuccessStatusCode)
                    {
                        Taches = await Http.GetFromJsonAsync<List<Tache>>("taches")
                            ?? throw new HttpRequestException();
                        SnackBar.Add("J'ai faim", Severity.Success);
                        StateHasChanged();
                    }
                    else
                    {
                        SnackBar.Add("Dépeche toi, J'ai faim", Severity.Error);
                        // afficher une message d'erreur
                    }

                }
            }
        }
    }
}
