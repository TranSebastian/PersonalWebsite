using Website.Components;
using Website.Components.Data;
using Website.Components.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

/*
 * Swagger stuff
 */
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

/*
 * Image database related items
 */
builder.Services.AddSqlite<ImageContext>("Data Source=WebsiteImages.db");
builder.Services.AddScoped<ImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

/*
 * No idea what any of this does
 * but i think i need all of it...
 */
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

/*
 * i think stuff to get db working
 */
app.UseAuthorization();
app.MapControllers();

// Add the CreateDbIfNotExists method call
app.CreateDbIfNotExists();

/*
 * Swagger stuff
 * Un comment to test image API
 */
//app.UseSwagger();
//app.UseSwaggerUI();
//app.MapGet("/", () => @"Contoso Pizza management API. Navigate to /swagger to open the Swagger test UI.");

app.Run();