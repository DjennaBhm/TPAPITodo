var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Api_TodoContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Seed data into the database using a scoped context
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Api_TodoContext>();
    await SeedData.InitializeAsync(context); // Asynchronous seeding
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // This will map all controller routes including AgendaController

app.Run();
