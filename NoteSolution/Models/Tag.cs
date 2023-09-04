using System.ComponentModel.DataAnnotations;

namespace NoteSolution.Models;

public class Tag
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation property for many-to-many relationship with Note
    public List<Note> Notes { get; } = new();
}