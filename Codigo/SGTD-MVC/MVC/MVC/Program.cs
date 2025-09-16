var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient("ProveedoresApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("ProductosApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("ReportesApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("UsuariosApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("RubrosApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("RolesApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("DisciplinasApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
