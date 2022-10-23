using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizerContext _context;

        public TarefaController(OrganizerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a todo, retornar NotFound,
            // caso contrário retornar OK com a todo encontrada
            return Ok();
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as todos no banco utilizando o EF
            return Ok();
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as todos no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok();
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var todo = _context.Todos.Where(x => x.StartDate.Date == data.Date);
            return Ok(todo);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(TodoStatusEnum status)
        {
            // TODO: Buscar  as todos no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var todo = _context.Todos.Where(x => x.Status == status);
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Criar(Todo todo)
        {
            if (todo.StartDate == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da todo não pode ser vazia" });

            // TODO: Adicionar a todo recebida no EF e salvar as mudanças (save changes)
            return CreatedAtAction(nameof(ObterPorId), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Todo todo)
        {
            var tarefaBanco = _context.Todos.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (todo.StartDate == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da todo não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a todo recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Todos.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a todo encontrada através do EF e salvar as mudanças (save changes)
            return NoContent();
        }
    }
}
