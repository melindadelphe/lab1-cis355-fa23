public class AuthorService : IAuthorService
{
    private readonly List<Author> _authors;

    public AuthorService()
    {
    Author = new List <Author>
        {
            new Author { Id = 1, Title = "1984", Author = "George Orwell" },
            new Author { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
            new Author { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
        };
    }

    public IEnumerable Author> GetAllAuthor() =>Author;

    public Author GetAuthorById(int id) =>Author.FirstOrDefault(b => b.Id == id);

    public void AddAuthor Author Author)
    {
    Author.Add(Author);
    }

    public void UpdateAuthor Author Author)
    {
        var existingAuthor = GetAuthorById(Author.Id);
        if (existingAuthor == null)
        {
            return;
        }
        existingAuthor.Title = Author.Title;
        existingAuthor.Author = Author.Author;
    }

    public void DeleteAuthor(int id)
    {
        var Author = GetAuthorById(id);
        if (Author == null)
        {
            return;
        }
    Author.Remove(Author);
    }
}