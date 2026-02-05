using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence;

public class SubmissionDbContext : DbContext
{

    #region Entities

    /*
     DbSet properties in your DbContext are typically declared as virtual
        1. Lazy Loading
        2. Change Tracking and Dynamic Proxies
        3. Unit Testing/Mocking
        4. Custom Implementations

    Best Practices:
        For most applications: Keep DbSet properties virtual for flexibility
        If using lazy loading: Navigation properties in entity classes must be virtual
        For performance-critical apps: Avoid lazy loading altogether; use eager loading
        For unit testing: Virtual properties are easier to mock
     */

    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<Journal> Journals { get; set; }

    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<ArticleAuthor> ArticleAuthors { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
