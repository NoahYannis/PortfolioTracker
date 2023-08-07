using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioTrackerClient;
using PortfolioTrackerClient.Services.AuthService;
using PortfolioTrackerClient.Services.GetStockInfoService;
using PortfolioTrackerClient.Services.PortfolioService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IGetStockInfoService, GetStockInfoServiceBlazor>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddBlazoredModal();

await builder.Build().RunAsync();
