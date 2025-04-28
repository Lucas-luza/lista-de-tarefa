using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tarefas_backend.Data;
using tarefas_backend.Models;

namespace tarefas_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tarefas = await _context.Tarefas.ToListAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tarefa novaTarefa)
        {
            novaTarefa.DataCriacao = DateTime.Now;
            _context.Tarefas.Add(novaTarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = novaTarefa.Id }, novaTarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Tarefa atualizada)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();

            tarefa.Descricao = atualizada.Descricao;
            tarefa.DataConclusao = atualizada.DataConclusao;
            tarefa.Responsavel = atualizada.Responsavel;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}