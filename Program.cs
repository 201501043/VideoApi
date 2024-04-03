using Microsoft.EntityFrameworkCore;
using VideoApi.Interface;
using VideoApi.Models;
using VideoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy => policy.AddPolicy("allowAll", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
builder.Services.AddDbContext<VideoUploadContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<ITemporaryChunk, TemporaryChunk>();
builder.Services.AddScoped<IVideoMetaData, VideoApi.Services.VideoMetaData>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
