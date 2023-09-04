using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using NoteSolution.Models;

namespace NoteSolution.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        

        
        builder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Personal" },
            new Tag { Id = 2, Name = "Work" },
            new Tag { Id = 3, Name = "Travel" },
            new Tag { Id = 4, Name = "Food" },
            new Tag { Id = 5, Name = "Health" },
            new Tag { Id = 6, Name = "Technology" },
            new Tag { Id = 7, Name = "Entertainment" },
            new Tag { Id = 8, Name = "Sports" },
            new Tag { Id = 9, Name = "Education" },
            new Tag { Id = 10, Name = "Other" }
        );    
        
        builder.Entity<Note>().HasData(
            new Note
            {
                Id = 1,
                Title = "Meeting Notes",
                Content = "Discussion about project timelines.",
                IsArchived = false
            },
            new Note
            {
                Id = 2,
                Title = "Grocery List",
                Content = "Buy milk, eggs, and bread.",
                IsArchived = false
            },
            new Note
            {
                Id = 3,
                Title = "Personal Journal",
                Content = "Today's thoughts and reflections.",
                IsArchived = false
            },
            new Note
            {
                Id = 4,
                Title = "Work Task",
                Content = "Finish the quarterly report.",
                IsArchived = false
            },
            new Note
            {
                Id = 5,
                Title = "Travel Plans",
                Content = "Book flights and hotels for the vacation.",
                IsArchived = false
            },
            new Note
            {
                Id = 6,
                Title = "Recipes",
                Content = "Pasta recipe for dinner.",
                IsArchived = false
            },
            new Note
            {
                Id = 7,
                Title = "Fitness Goals",
                Content = "Track daily workouts and progress.",
                IsArchived = false
            },
            new Note
            {
                Id = 8,
                Title = "Coding Project",
                Content = "Implement new features for the app.",
                IsArchived = false
            },
            new Note
            {
                Id = 9,
                Title = "Book Recommendations",
                Content = "Books to read in the next month.",
                IsArchived = false
            },
            new Note
            {
                Id = 10,
                Title = "Shopping List",
                Content = "Items needed for the weekend BBQ.",
                IsArchived = false
            }
        );
        
        var hasher = new PasswordHasher<IdentityUser>();

        
        builder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "1", // Replace with a unique ID (usually a GUID)
                UserName = "admin@example.com", // Set the username or email
                NormalizedUserName = "ADMIN@EXAMPLE.COM", // Normalize the username
                Email = "admin@example.com", // Set the email address
                NormalizedEmail = "ADMIN@EXAMPLE.COM", // Normalize the email
                EmailConfirmed = true, // Email is confirmed
                PasswordHash = hasher.HashPassword(null, "Dotnet@example"), // Set the password hash
                SecurityStamp = string.Empty
            }
        );
    }


    public DbSet<Note> Notes { get; set; }
    public DbSet<Tag> Tags { get; set; }

}

