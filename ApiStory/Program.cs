using ApiStory.Data;
using ApiStory.Handlers;
using ApiStory.Requests;
using ApiStory.Response;
using ApiStory.Service;
using ApiStory.Service.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(p => p.AddPolicy("AllowOrigin", build =>
{
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRequestHandler<CreateStoryRequest, CreateStoryResponse>, CreateStoryHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateStoryRequest, bool>, UpdateStoryHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteStoryRequest, bool>, DeleteStoryHandler>();
builder.Services.AddScoped<IRequestHandler<FindAllStoryRequest, List<StoryResponse>>, FindAllStoryHandler>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
