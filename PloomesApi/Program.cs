using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniValidation;
using PloomesApi.Data;
using PloomesApi.DTO;
using PloomesApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PloomesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Teste de API Genérica Ploomes",
        Description = "Candidato André Pereira",
        Contact = new OpenApiContact { Name = "André Pereira", Email = "pereira.al.andre@gmail.com" },
        License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Rotas

app.MapGet("usuarios", async (PloomesDbContext context) =>
{
    var result = await context.Usuarios.ToListAsync();

    return result;
})
    .Produces<List<Usuario>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("listarUsuarios")
    .WithTags("Usuario");

app.MapGet("usuario/{id}", async (Guid Id, PloomesDbContext context) =>
{
    var result = await context.Usuarios.FindAsync(Id);

    return result;
})
    .Produces<UsuarioDTO>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("buscarUsuario")
    .WithTags("Usuario");

app.MapPost("usuario", async ([FromBody] UsuarioDTO usuarioDTO, PloomesDbContext context) =>
{


    var usuario = Usuario.Novo(usuarioDTO.Nome, usuarioDTO.Email);

    if (!MiniValidator.TryValidate(usuario, out var errors))
        return Results.ValidationProblem(errors);

    context.Usuarios.Add(usuario);

    var result = await context.SaveChangesAsync();

    return result > 0 ? Results.CreatedAtRoute("listarUsuarios") : Results.BadRequest();

})
    .ProducesValidationProblem()
    .Produces<UsuarioDTO>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("criar")
    .WithTags("Usuario");

#endregion

app.Run();
