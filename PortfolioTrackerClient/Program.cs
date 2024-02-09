global using Blazored.LocalStorage;
global using Blazored.Modal;
global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using Microsoft.Extensions.Localization;
global using PortfolioTrackerClient;
global using PortfolioTrackerClient.Resources;
global using PortfolioTrackerClient.Services.AuthService;
global using PortfolioTrackerClient.Services.GetStockInfoService;
global using PortfolioTrackerClient.Services.PortfolioService;
global using PortfolioTrackerShared.Models.UserModels;
global using PortfolioTrackerShared.Other;
using PortfolioTrackerClient.Services.EmailService;
using PortfolioTrackerClient.Services.SettingsService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddLocalization();
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IGetStockInfoService, GetStockInfoServiceBlazor>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddBlazoredModal();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
