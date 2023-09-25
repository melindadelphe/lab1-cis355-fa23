using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

# region Swagger / Build
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1"});
});

// This line registers the IAuthorService class with the application's "services container."
// The 'AddSingleton' method tells the application to create one instance of IAuthorService and
// use that same instance every time the service is needed. This way, you don't have to manually
// create a new IAuthorService each time you need to use it; the system will automatically give you
// the one it has created. This is a technique called "Dependency Injection."
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json","My API");
});

# endregion

app.MapGet("/", () => "Hello World!");

// Mapping an HTTP GET request to the "/Author" endpoint.
// When someone navigates to this URL, the list of all Author will be returned.
app.MapGet("/Author", (IAuthorService AuthorService) => {
    return AuthorService.GetAllAuthor();
});

// Mapping an HTTP GET request to the "/Author/{id}" endpoint.
// The {id} in the URL is a parameter, meaning it will be replaced by the ID of the Author you're looking to get.
// For example, "/Author/1" will look for the Author with an ID of 1.
app.MapGet("/Author/{id}", (int id, IAuthorService AuthorService) => 
{
    // Use LINQ's FirstOrDefault method to search for a Author with a matching ID in the list.
    // If no Author is found, 'Author' will be null.
    var Author = AuthorService.GetAuthorById(id);

    // Check if the Author was found.
    if (Author == null) return Results.NotFound();

    // If found, return the Author along with a 200 OK status.
    return Results.Ok(Author);
});

// Map a POST request to the "/Author" endpoint.
// This will allow clients to create new Author.
// The new Author's data will be passed in the request body and mapped to the 'newAuthor' parameter.
app.MapPost("/Author", (Author newAuthor, IAuthorService AuthorService) =>
{
    // Add the new Author to the list of Author in the IAuthorService class.
    AuthorService.AddAuthor(newAuthor);

    // Return a 201 Created status along with the newly created Author.
    // The Created status code indicates that the resource was successfully created.
    return Results.Created($"/Author/{newAuthor.Id}", newAuthor);
});

// Map a PUT request to the "/Author/{id}" endpoint.
// This will allow clients to update an existing Author identified by its ID.
// The Author's new data will be passed in the request body and mapped to the 'updatedAuthor' parameter.
app.MapPut("/Author/{id}", (int id, Author updatedAuthor, IAuthorService AuthorService) =>
{
    AuthorService.UpdateAuthor(updatedAuthor);

    // Return a 200 OK status along with the updated Author.
    // The OK status code indicates that the request was successful.
    return Results.Ok(updatedAuthor);
});

// Map a DELETE request to the "/Author/{id}" endpoint.
// This will allow clients to delete an existing Author identified by its ID.
app.MapDelete("/Author/{id}", (int id, IAuthorService AuthorService) =>
{
    AuthorService.DeleteAuthor(id);

    // Return a 200 OK status along with the DeleteAuthor Author.
    // The OK status code indicates that the request was successful.
    return Results.Ok(null);

});











app.MapGet("/Author", (IAuthorService AuthorService) => {
    return AuthorService.GetAllAuthor();
});


app.MapGet("/Author/{id}", (int id, IAuthorService AuthorService) => 
{
    var Author = AuthorService.GetAuthorById(id);

    if (Author == null) return Results.NotFound();

    return Results.Ok(Author);
});

app.MapPost("/Author", (Author newAuthor, IAuthorService AuthorService) =>
{
    AuthorService.AddAuthor(newAuthor);

    return Results.Created($"/Author/{newAuthor.Id}", newAuthor);
});

app.MapPut("/Author/{id}", (int id, updatedAuthor, IAuthorService AuthorService) =>
{
    AuthorService.UpdateAuthor(updatedAuthor);

    return Results.Ok(UpdateAuthor);
});

app.MapDelete("/Author/{id}", (int id, IAuthorService AuthorService) =>
{
    AuthorService.DeleteAuthor(id);

    return Results.Ok(null);

});

app.Run();

