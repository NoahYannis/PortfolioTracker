global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
global using Blazored.Modal;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using PortfolioTrackerClient;
global using PortfolioTrackerClient.Services.AuthService;
global using PortfolioTrackerClient.Services.GetStockInfoService;
global using PortfolioTrackerClient.Services.PortfolioService;
global using PortfolioTrackerShared.Models.UserModels;
global using PortfolioTrackerShared.Other;
global using Microsoft.Extensions.Localization;
using PortfolioTrackerClient.Services.SettingsService;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

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
builder.Services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();




// Configure supported cultures
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("de-DE"),
};

// Create and configure the localizer
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();


//builder.Services.AddLocalization();



await builder.Build().RunAsync();
