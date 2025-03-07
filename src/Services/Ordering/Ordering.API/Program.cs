var builder = WebApplication.CreateBuilder(args);

// Add services to the continer

var app = builder.Build();

// configure the HTTP request pipeline

app.Run();
