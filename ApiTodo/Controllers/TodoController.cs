using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly Api_TodoContext _context;
    public TodoController(Api_TodoContext context)
    {
        _context = context;
    }
    // GET: api/todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Api_Todo>>> GetTodos()
    {
        // Get items
        var todos = _context.Api_Todo;
        return await _context.Api_Todo.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Api_Todo>> GetTodoById(int id)
    {
        var todo = await _context.Api_Todo.FindAsync(id);
        return todo;
    }
    [HttpPost]
    public async Task<ActionResult<Api_Todo>> CreateTodo(Api_Todo todo)
    {
        _context.Api_Todo.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }
    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, Api_Todo todo)
    {
        if (id != todo.Id)
        {
            return BadRequest();
        }

        _context.Entry(todo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id))
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

    // DELETE: api/todo/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.Api_Todo.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        _context.Api_Todo.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoExists(int id)
    {
        return _context.Api_Todo.Any(e => e.Id == id);
    }

}
