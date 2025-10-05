using JokesAppUI.Components;
using JokesAppUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<JokeService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<AudienceService>();



builder.Services.AddHttpClient("JokesAPI", client =>
{
    client.BaseAddress = new Uri("https://jokes-atdja0dvhndmdkaq.canadacentral-01.azurewebsites.net/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
