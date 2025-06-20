using Blazored.SessionStorage;
using Hutech.Exam.Client;
using Hutech.Exam.Client.API;
using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Client.BUS;
using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("Hutech.Exam.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Hutech.Exam.ServerAPI"));
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<ISenderAPI, SenderAPI>();
builder.Services.AddAuthorizationCore();
// bien toan cuc
builder.Services.AddSingleton<AdminDataService>();
builder.Services.AddSingleton<ApplicationDataService>();
builder.Services.AddSingleton<AdminHubService>();
builder.Services.AddSingleton<StudentHubService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// them MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

    //Gi?i h?n s? l??ng Snackbar
    config.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
});

builder.Services.AddScoped<ApiService>();
await builder.Build().RunAsync();
