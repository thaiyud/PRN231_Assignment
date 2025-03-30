using SchoolPsychologicalHealthSupportSystem_API.GraphQL;
using SchoolPsychologicalHealthSupportSystem_Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register GraphQL services
builder.Services.AddScoped<IBlogService, BlogService>();

// Configure GraphQL
builder.Services.AddGraphQLServer().AddQueryType<BlogsQuery>().BindRuntimeType<DateTime, DateTimeType>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting().UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
app.MapControllers();
app.Run();