var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()

    // NOTE: La siguiente instrucción nos permitirá cargar vistas razor de otros proyectos, para ello necesitamos:
    //           1.- Instalar el nuget "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation".
    //           2.- Especificar el tag "EmbeddedResource" en el csproj del proyecto que contiene las vistas.
    .AddRazorRuntimeCompilation(options =>
    {
        options.FileProviders.Add(new Microsoft.Extensions.FileProviders.EmbeddedFileProvider(
            // NOTE: Tenemos que cargar el assembly del proyecto que contiene las vistas (en mi caso, el proyecto es "Web.Master.Views").
            System.Reflection.Assembly.Load(new System.Reflection.AssemblyName("Web.Master.Views"))
            ));
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
