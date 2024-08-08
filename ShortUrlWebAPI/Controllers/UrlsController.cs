using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using ShortUrlWebAPI.Data;

[ApiController]
[Route("api/[controller]")]
public class UrlsController : ControllerBase
{
    private readonly AppDbContext _context;

    public UrlsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<URL>>> GetUrls()
    {
        return await _context.Urls.ToListAsync();
    }

    // Add other CRUD operations as needed
}
