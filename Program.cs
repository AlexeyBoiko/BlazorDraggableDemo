using BlazorDraggableDemo;
using BlazorDraggableDemo.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => 
	new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
	.AddSingleton<MouseService>()
	.AddSingleton<IMouseService>(ff => ff.GetRequiredService<MouseService>());

await builder.Build().RunAsync();
