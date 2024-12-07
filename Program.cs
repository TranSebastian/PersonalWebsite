using Website.Components;
using Website.Components.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

/*
 * Swagger stuff
 */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

/*
 * Image database related items
 */
builder.Services.AddSqlite<ImageContext>("Data Source=WebsiteImages.db");

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
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Add the CreateDbIfNotExists method call
app.CreateDbIfNotExists();

/*
 * More swagger stuff
 * go to /swagger to test API
 */
app.UseSwagger();
app.UseSwaggerUI();

app.Run();