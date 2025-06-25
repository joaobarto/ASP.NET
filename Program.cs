using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext para MySQL
builder.Services.AddDbContext<TarefasContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

var app = builder.Build();

// Aplica migrations automaticamente (cria o banco/tabelas se não existirem)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TarefasContext>();
    db.Database.Migrate();
}

app.UseDefaultFiles(); // Serve index.html por padrão
app.UseStaticFiles(); // Permite servir arquivos da pasta wwwroot

// Endpoint para listar tarefas
app.MapGet("/api/tarefas", async (TarefasContext db) =>
    await db.Tarefas.OrderByDescending(t => t.DataCriacao).ToListAsync()
);

// Endpoint para adicionar tarefa
app.MapPost("/api/tarefas", async (TarefasContext db, Tarefa tarefa) =>
{
    tarefa.DataCriacao = DateTime.Now;
    db.Tarefas.Add(tarefa);
    await db.SaveChangesAsync();
    return Results.Created($"/api/tarefas/{tarefa.Id}", tarefa);
});

// Endpoint para marcar como concluída
app.MapPut("/api/tarefas/{id}/concluir", async (TarefasContext db, int id) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa is null) return Results.NotFound();
    tarefa.Concluida = true;
    await db.SaveChangesAsync();
    return Results.Ok(tarefa);
});

// Endpoint para remover tarefa
app.MapDelete("/api/tarefas/{id}", async (TarefasContext db, int id) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa is null) return Results.NotFound();
    db.Tarefas.Remove(tarefa);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/sobre", async context =>
{
    await context.Response.SendFileAsync("wwwroot/sobre.html");
});

app.Run();

// Modelo e contexto EF Core
public class Tarefa
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public bool Concluida { get; set; }
    public DateTime DataCriacao { get; set; }
}

public class TarefasContext : DbContext
{
    public TarefasContext(DbContextOptions<TarefasContext> options) : base(options) { }
    public DbSet<Tarefa> Tarefas { get; set; }
}

// (nenhuma alteração necessária aqui, apenas garanta que o nome do banco está igual ao da connection string e do script SQL)
