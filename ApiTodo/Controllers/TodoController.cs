using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly Api_TodoContext _context;

    public TodoController(Api_TodoContext context)
    {
        _context = context;
    }

    // // GET: api/todo/agendas
    // [HttpGet("agendas")]
    // [SwaggerOperation(Summary = "Get all Agendas", Description = "Retrieves the list of all Agendas items.")]
    // [SwaggerResponse(StatusCodes.Status200OK, "List of Agendas items retrieved", typeof(IEnumerable<Agenda>))]
    // public async Task<ActionResult<IEnumerable<Agenda>>> GetAgendas()
    // {
    //     var agendas = await _context.Agendas.ToListAsync();
    //     return Ok(agendas);
    // }

    // GET: api/todo
    [HttpGet]
    [SwaggerOperation(Summary = "Get all Todos", Description = "Retrieves the list of all Todo items.")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of Todo items retrieved", typeof(IEnumerable<Api_Todo>))]
    public async Task<ActionResult<IEnumerable<Api_Todo>>> GetTodos()
    {
        return await _context.Api_Todo.ToListAsync();
    }

    // GET: api/todo/{id}
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a Todo by ID", Description = "Retrieves a specific Todo item by its unique identifier.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Todo item found", typeof(Api_Todo))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Todo item not found")]
    public async Task<ActionResult<Api_Todo>> GetTodoById([SwaggerParameter("The unique identifier of the Todo", Required = true)] int id)
    {
        var todo = await _context.Api_Todo.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    // POST: api/todo
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new Todo", Description = "Creates a new Todo item and saves it to the database.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Todo item successfully created", typeof(Api_Todo))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data provided")]
    public async Task<ActionResult<Api_Todo>> CreateTodo([FromBody] Api_Todo todo)
    {
        if (todo == null || !ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Ajoutez la tâche à la base de données
        _context.Api_Todo.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }


    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update an existing Todo", Description = "Updates an existing Todo item by its ID.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Todo item successfully updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Todo item not found")]
    public async Task<IActionResult> UpdateTodo([SwaggerParameter("The unique identifier of the Todo to update", Required = true)] int id, [FromBody] Api_Todo todo)
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
    [SwaggerOperation(Summary = "Delete a Todo by ID", Description = "Deletes a specific Todo item by its unique identifier.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Todo item successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Todo item not found")]
    public async Task<IActionResult> DeleteTodo([SwaggerParameter("The unique identifier of the Todo to delete", Required = true)] int id)
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

    // Helper method to check if a Todo exists
    private bool TodoExists(int id)
    {
        return _context.Api_Todo.Any(e => e.Id == id);
    }
}
