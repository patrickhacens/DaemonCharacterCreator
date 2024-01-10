using CharacterManager;
using Daemon.Converters;
using Daemon.Models;
using DnetIndexedDb;
using DnetIndexedDb.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton(ConfigureJOptions(new JsonSerializerOptions()));

builder.Services
    .AddIndexedDbDatabase<Db>(options => options
    .UseDatabase(new IndexedDbDatabaseModel
    {
        Name = "Daemon Character Manager",
        Version = 1,
        Stores = {
            new IndexedDbStore
            {
                Name = nameof(Player),
                Key = new IndexedDbStoreParameter
                {
                    AutoIncrement = false,
                    KeyPath = nameof(Player.Name)
                },
                Indexes = new List<IndexedDbIndex>()
            }
        }
    }));



var app = builder.Build();

UpdateJSRuntimeJserialize(app.Services);

await app.Services.GetService<Db>()!.OpenIndexedDb();

await app.RunAsync();



void UpdateJSRuntimeJserialize(IServiceProvider services)
{
    try
    {
        var jsRuntime = services.GetService<IJSRuntime>();
        var prop = typeof(JSRuntime).GetProperty("JsonSerializerOptions", BindingFlags.NonPublic | BindingFlags.Instance);
        JsonSerializerOptions options = (JsonSerializerOptions)Convert.ChangeType(prop.GetValue(jsRuntime, null), typeof(JsonSerializerOptions))!;
        ConfigureJOptions(options);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"SOME ERROR: {ex}");
    }
}

JsonSerializerOptions ConfigureJOptions(JsonSerializerOptions jOptions)
{
    jOptions.Converters.Add(new PlayerConverter());
    jOptions.Converters.Add(new ModifierConverter());
    jOptions.Converters.Add(new SkillConverter());
    jOptions.Converters.Add(new AttributeConverter());
    jOptions.Converters.Add(new JsonStringEnumConverter());
    jOptions.IgnoreReadOnlyProperties = true;
    jOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    return jOptions;
}