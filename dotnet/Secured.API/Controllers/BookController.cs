using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secured.API.Domain;
using Secured.API.Persistence.Interfaces;

namespace Secured.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly IApplicationDbContext _context;

    public BookController(ILogger<BookController> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get request completed successfully");

        var books = await _context.Books.ToListAsync(cancellationToken);

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get request completed successfully");

        var book = await _context.Books
            .FindAsync(new object[] { id }, cancellationToken);

        if (book is null)
        {
            return BadRequest("Item not found with given id");
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Book book, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add request completed successfully");

        var entity = _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok(entity.Entity.Id);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Book book, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add request completed successfully");

        var entity = await _context.Books
            .FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
        {
            return BadRequest("Item not found with given id");
        }

        entity.Name = book.Name;
        
        await _context.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add request completed successfully");

        var entity = await _context.Books
            .FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
        {
            return BadRequest("Item not found with given id");
        }

        _context.Books.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}