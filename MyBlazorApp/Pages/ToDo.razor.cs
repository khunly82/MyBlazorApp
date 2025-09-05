using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyBlazorApp.Components;
using MyBlazorApp.Models;
using System.Net.Http.Json;

namespace MyBlazorApp.Pages
{
    public partial class ToDo
    {
        [Inject]
        public IDialogService DialogService { get; set; } = null!;

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
