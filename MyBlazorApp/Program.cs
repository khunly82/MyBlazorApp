using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using MudBlazor.Services;
using MyBlazorApp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri("http://10.10.5.126:3000") 
});

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, MyAuthStateProvider>();

await builder.Build().RunAsync();

class MyAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime Js;
    private string? token;

    public string? Token
    {
        get { return token; }
        set { 
            token = value; 
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    public MyAuthStateProvider(IJSRuntime js)
    {
        Js = js;
        LoadStorage();
    }

    private async void LoadStorage()
    {
        Token = await Js.InvokeAsync<string?>("localStorage.getItem", ["token"]);
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            await Js.InvokeVoidAsync("localStorage.setItem", ["token", token]);
            JwtSecurityTokenHandler handler = new();
            ClaimsPrincipal principal = new ([
                new ClaimsIdentity(
                    handler.ReadJwtToken(Token).Claims,
                    authenticationType: "jwt"
                )
            ]);
            return new AuthenticationState(principal);
        }
        catch (Exception)
        {
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
