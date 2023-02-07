using FrontEndToDemoObservable;
using FrontEndToDemoObservable.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("baseAPIClient",
    client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("URL:BaseApi")))
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, t => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("cohortAPIClient",
    client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("URL:CohortApi")))
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, t => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("psiAPIClient",
    client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("URL:PsiUrl")))
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, t => TimeSpan.FromMilliseconds(500)));

builder.Services.AddSingleton<StateContainer>();
builder.Services.AddSingleton<SessionApiService>();

await builder.Build().RunAsync();
