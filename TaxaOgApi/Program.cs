using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxaOgApi;



var builder = WebAssemblyHostBuilder.CreateDefault(args);


//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        policy =>
//        {
//            policy.AllowAnyOrigin();  // Sæt den tilladte oprindelse
//            policy.AllowAnyMethod();  // Tillad enhver HTTP-metode (GET, POST, PUT, osv.)
//            policy.AllowAnyHeader();  // Tillad enhver header i anmodningen
//        });
//});

builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Build().RunAsync();