using Application;
using Application.Hubs;
using Persistence;
using Voter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.Filters.Add(new ApplicationExceptionFilter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSignalR();

var origins = new[]
{
    "http://localhost:4200"
};

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p =>
{
    p.WithOrigins(origins);
    p.AllowAnyMethod();
    p.AllowAnyHeader();
    p.AllowCredentials();
}));

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<VoteHub>("VoteHub");

app.Run();
