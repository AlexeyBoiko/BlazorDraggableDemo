using BlazorDraggableDemo.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BlazorDraggableDemo {
	public class Program {
		public static async Task Main(string[] args) {
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services
				.AddSingleton<MouseService>()
				.AddSingleton<IMouseService>(ff => ff.GetRequiredService<MouseService>());

			await builder.Build().RunAsync();
		}
	}
}
