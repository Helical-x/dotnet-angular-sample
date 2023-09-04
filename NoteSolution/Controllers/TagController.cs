using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteSolution.Data;
using NoteSolution.Models;

namespace NoteSolution.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TagController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Tag>> GetTags()
    {
        return await _context.Tags.ToArrayAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Tag>> PostTag(Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}