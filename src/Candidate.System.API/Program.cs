using Candidate.System.Application.Interfaces;
using Candidate.System.Application.Services;
using Candidate.System.Infrastructure.Data;
using Candidate.System.Infrastructure.Repositories;
using Candidate.System.API.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR
builder.Services.AddSignalR();

// Add Entity Framework
builder.Services.AddDbContext<CandidateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
        "Server=127.0.0.1,1433;Database=CandidateSystemDb;User Id=sa;Password=Strong.Pwd-123;TrustServerCertificate=True;Encrypt=True;"));

// Add repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();

// Add application services
builder.Services.AddScoped<ISelectionService, SelectionService>();
builder.Services.AddScoped<IStreamingService, StreamingService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();
app.MapHub<SelectionHub>("/selectionHub");

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CandidateDbContext>();
    await context.Database.EnsureCreatedAsync();
    
    var streamingService = scope.ServiceProvider.GetRequiredService<IStreamingService>();
    await streamingService.StartStreamingAsync();
}

app.Run();