using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObterFuncionarios()
        {
            var funcionarios = await _context.Funcionarios.ToListAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterFuncionarioPorId(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarFuncionario([FromBody] Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            // Log da ação
            var log = new FuncionarioLog { Acao = "Cadastro", DataAcao = DateTime.Now };
            _context.FuncionarioLogs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterFuncionarioPorId), new { id = funcionario.Id }, funcionario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFuncionario(int id, [FromBody] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            _context.Entry(funcionario).State = EntityState.Modified;

            // Log da ação
            var log = new FuncionarioLog { Acao = "Atualização", DataAcao = DateTime.Now };
            _context.FuncionarioLogs.Add(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionario);

            // Log da ação
            var log = new FuncionarioLog { Acao = "Exclusão", DataAcao = DateTime.Now };
            _context.FuncionarioLogs.Add(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
