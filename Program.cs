using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

# region Swagger / Build
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1"});
});

// This line registers the IBookService class with the application's "services container."
// The 'AddSingleton' method tells the application to create one instance of IBookService and
// use that same instance every time the service is needed. This way, you don't have to manually
// create a new IBookService each time you need to use it; the system will automatically give you
// the one it has created. This is a technique called "Dependency Injection."
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json","My API");
});

# endregion

app.MapGet("/", () => "Hello World!");

// Mapping an HTTP GET request to the "/Book" endpoint.
// When someone navigates to this URL, the list of all Book will be returned.
app.MapGet("/Book", (IBookService BookService) => {
    return BookService.GetAllBooks();
});

// Mapping an HTTP GET request to the "/Books/{id}" endpoint.
// The {id} in the URL is a parameter, meaning it will be replaced by the ID of the Books you're looking to get.
// For example, "/Book/1" will look for the Book with an ID of 1.
app.MapGet("/Book/{id}", (int id, IBookService BookService) => 
{
    // Use LINQ's FirstOrDefault method to search for a Book with a matching ID in the list.
    // If no Book is found, 'Book' will be null.
    var Book = BookService.GetBookById(id);

    // Check if the Book was found.
    if (Book == null) return Results.NotFound();

    // If found, return the Book along with a 200 OK status.
    return Results.Ok(Book);
});

// Map a POST request to the "/Book" endpoint.
// This will allow clients to create new Book.
// The new Book's data will be passed in the request body and mapped to the 'newBook' parameter.
app.MapPost("/Book", (Book newBook, IBookService BookService) =>
{
    // Add the new Book to the list of Book in the IBookService class.
    BookService.AddBook(newBook);

    // Return a 201 Created status along with the newly created Book.
    // The Created status code indicates that the resource was successfully created.
    return Results.Created($"/Author/{newBook.Id}", newBook);
});

// Map a PUT request to the "/Book/{id}" endpoint.
// This will allow clients to update an existing Book identified by its ID.
// The Book's new data will be passed in the request body and mapped to the 'updatedBook' parameter.
app.MapPut("/Book/{id}", (int id, Book updatedBook, IBookService BookService) =>
{
    BookService.updatedBook(updatedBooks);

    // Return a 200 OK status along with the updated Books.
    // The OK status code indicates that the request was successful.
    return Results.Ok(updatedBook);
});

// Map a DELETE request to the "/Book/{id}" endpoint.
// This will allow clients to delete an existing Book identified by its ID.
app.MapDelete("/Book/{id}", (int id, IBookService BookService) =>
{
    BookService.DeleteBook(id);

    // Return a 200 OK status along with the DeleteBook Books.
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

