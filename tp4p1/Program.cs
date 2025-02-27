using Microsoft.EntityFrameworkCore;
using tp4p1.Models.DataManager;
using tp4p1.Models.EntityFramework;
using tp4p1.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Ajouter la configuration du DbContext
builder.Services.AddDbContext<FilmRatingsDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();

// Ajouter les services au conteneur.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurer le pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
