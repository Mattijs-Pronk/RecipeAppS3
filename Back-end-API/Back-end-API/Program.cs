using Back_end_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();

    }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//toegevoegt om opties toe te voegen aan de DB en te connecten met de Sqlserver.
builder.Services.AddDbContext<RecipeAppContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/StaticFiles"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
