using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskManagerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            var items = await _unitOfWork.ToDoItems.GetAll();
            return Ok(items);
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var item = await _unitOfWork.ToDoItems.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // PUT: api/ToDoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            _unitOfWork.ToDoItems.Update(toDoItem);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
            await _unitOfWork.ToDoItems.Add(toDoItem);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var toDoItem = await _unitOfWork.ToDoItems.GetById(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            await _unitOfWork.ToDoItems.Delete(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
