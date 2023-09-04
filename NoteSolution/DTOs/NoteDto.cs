namespace NoteSolution.DTOs;

public class NoteDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public bool IsArchived { get; set; } = false;
    
    // Navigation property for many-to-many relationship with Tag
    public List<TagDto> Tags { get; set; }
}