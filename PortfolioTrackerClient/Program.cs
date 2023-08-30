global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
global using Blazored.Modal;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using PortfolioTrackerClient;
global using PortfolioTrackerClient.Services.AuthService;
global using PortfolioTrackerClient.Services.GetStockInfoService;
global using PortfolioTrackerClient.Services.PortfolioService;
using PortfolioTrackerClient.Services.SettingsService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IGetStockInfoService, GetStockInfoServiceBlazor>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();

builder.Services.AddBlazoredModal();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
