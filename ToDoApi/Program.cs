// using Microsoft.EntityFrameworkCore;
// using ToDoApi;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddDbContext<ToDoDbContext>();
// builder.Services.AddSwaggerGen();
// builder.Services.AddSingleton<Service>();

// var app = builder.Build();

//  app.UseCors(
//         options => options.AllowAnyMethod()
//     );

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.MapGet("/todos", () => "Hello World!");
// app.MapGet("/{id}", (int id, Service service) => { });
// //app.MapGet("/todos/{id}", (int id) => "Hello World!");
// app.MapPost("/todos", (Item item) => "This is a POST");
// app.MapPut("/todos/{id}", (Item item) => "This is a PUT");
// app.MapDelete("/todos/{id}", (int id) => "This is a DELETE");

// app.Run();

using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using ToDoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API Name", Version = "v1" });
});

builder.Services.AddDbContext<ToDoDbContext>(
    // options => options.UseMySql("name=ToDoDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"))
    );

builder.Services.AddScoped<Service>();

var app = builder.Build();

//app.UseCors(options => options.AllowAnyMethod());
//app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

//app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = "/swagger";
});
}
app.MapGet("/", () => "Welcome to ToDoApi!");
// app.MapGet("/todos", () => {null});
// app.MapGet("/{id}", (int id, Service service) => { });
// app.MapPost("/todos", (Item item) => "This is a POST");
// app.MapPut("/todos/{id}", (Item item) => "This is a PUT");
// app.MapDelete("/todos/{id}", (int id) => "This is a DELETE");
app.MapGet("/todos", (Service service) => service.GetAllAsync());
//app.MapGet("/{id}", (int id, Service service) => service.ge(id));
app.MapPost("/todos", (Item item, Service service) => service.PostItemAsync(item));
app.MapPut("/todos/{id}", (Item item, int id, Service service) => service.PutItemAsync(id, item));
app.MapDelete("/todos/{id}", (int id, Service service) => service.DeleteItemAsync(id));

app.Run();