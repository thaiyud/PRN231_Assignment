using GraphQL;
using GraphQL.Types;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using SchoolPsychologicalHealthSupportSystem_Service;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add services to the container.
builder.Services.AddScoped<BlogMutation>();
builder.Services.AddScoped<CategoryMutation>();
builder.Services.AddScoped<BlogQuery>();
builder.Services.AddScoped<CategoryQuery>();
builder.Services.AddScoped<ISchema, AppSchema>(sp => new AppSchema(sp));

builder.Services.AddGraphQL(builder => 
{
    builder.AddSystemTextJson();
    builder.AddGraphTypes();
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7088") // Allow Blazor WebAssembly
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // If using authentication
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");

// Middleware GraphQL
app.UseGraphQL<ISchema>();
app.UseGraphQLPlayground("/ui/playground"); // Thêm Playground để test API GraphQL

app.UseAuthorization();
app.MapControllers();

app.Run();
