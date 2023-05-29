using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioTracker;
using PortfolioTracker.Services.GetStockInfoService;
using PortfolioTracker.Services.PortfolioService;
using PortfolioTracker.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IGetStockInfoService, GetStockInfoServiceBlazor>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();

await builder.Build().RunAsync();
