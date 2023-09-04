namespace NoteSolution.DTOs;

public class TagDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    // Navigation property for many-to-many relationship with Note
}