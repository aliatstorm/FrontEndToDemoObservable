using FrontEndToDemoObservable;
using FrontEndToDemoObservable.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
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
builder.Services.AddSingleton<SignalrClientService>();
builder.Services.AddSingleton(sp =>
{
    return new HubConnectionBuilder()
        .WithUrl(new Uri("https://localhost:7196/sessionhub"))
        .WithAutomaticReconnect()
        .Build();
});

await builder.Build().RunAsync();
