using ECommerce.Blazor.Components;
using ECommerce.Blazor.Services;
using ECommerce.Blazor.State;
var builder = WebApplication.CreateBuilder(args);

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<AppState>();
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5246/");
});


builder.Services.AddScoped<CartState>();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
