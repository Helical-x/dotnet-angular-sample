using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteSolution.Data;
using NoteSolution.DTOs;
using NoteSolution.Models;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace NoteSolution.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    
    private readonly ILogger<NoteController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public NoteController(ILogger<NoteController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Note>> GetNotes()
    {
        // Include related Tags when retrieving Notes
        var notes = await _context.Notes
            .Include(n => n.Tags)
            .ToArrayAsync();
        
        return notes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        
        if (note is null)
        {
            return NotFound();
        }

        return note;
    }

    [HttpPost]
    public async Task<ActionResult<Note>> PostNote(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutNote(int id, NoteDto noteDto)
    {

        if (id != noteDto.Id)
        {
            return BadRequest();
        }

        var note = await _context.Notes.FindAsync(id);

        note.Title = noteDto.Title;
        note.Content = noteDto.Content;
        note.IsArchived = noteDto.IsArchived;
        
        UpdateNoteTags(note, noteDto.Tags);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NoteExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        
        return NoContent();

    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound();
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool NoteExists(long id)
    {
        return _context.Notes.Any(e => e.Id == id);
    }
    
    private void UpdateNoteTags(Note note, List<TagDto> updatedTagIds)
    {
        // Clear existing tags and re-add the updated ones
        note.Tags.Clear();

        foreach (var tagId in updatedTagIds)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Id == tagId.Id);
            if (tag != null)
            {
                note.Tags.Add(tag); // Associate the note with the tag
            }
        }
    }

}