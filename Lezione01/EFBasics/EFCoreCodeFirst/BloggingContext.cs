using Microsoft.EntityFrameworkCore;

namespace EFCoreFirstApp
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }

        public string DbPath { get; }

        // Nota: di solito la Connection String viene messa in un file di settings (appsettings.json)
        public BloggingContext(string connectionString = "Data Source=.;Initial Catalog=BlogDB;Integrated Security=SSPI;MultipleActiveResultSets=True")
        {
            DbPath = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(DbPath);
    }

    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int? AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}