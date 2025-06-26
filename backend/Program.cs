using TodoListApp.Services;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariables("PORT") ?? "5238";

builder.WebHost.UseUrls($"http://*:{port}");
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<TodoService>();
builder.Services.AddControllers();
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowFrontend", policy => 
    {
        policy.WithOrigins(
            "http://localhost:5173"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
