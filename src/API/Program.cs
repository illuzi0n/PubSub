using API.Abstractions;
using API.Services;
using EventBus.Abstractions;
using EventBus.BasicEventBus;
using EventBus.BasicEventBus.EventHandler;
using EventBus.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventBus, BasicEventBus>();
builder.Services.AddTransient<ISomeService, SomeService>();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<SomeEvent>(new SomeEventHandler((string newJobDescription) => Console.WriteLine(newJobDescription)));

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
