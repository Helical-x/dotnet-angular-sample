using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace NoteSolution.Models;

public class Note
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }

    public bool IsArchived { get; set; } = false;
    
    // Navigation property for many-to-many relationship with Tag
    public List<Tag> Tags { get; } = new();
}