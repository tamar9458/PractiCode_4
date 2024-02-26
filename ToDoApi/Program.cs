using ToDoApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Service>();



var app = builder.Build();

app.MapGet("/todos", () => "Hello World!");
app.MapGet("/{id}", (int id, Service service) => { });
//app.MapGet("/todos/{id}", (int id) => "Hello World!");
app.MapPost("/todos", (Item item) => "This is a POST");
app.MapPut("/todos/{id}", (Item item) => "This is a PUT");
app.MapDelete("/todos/{id}", (int id) => "This is a DELETE");

app.Run();
