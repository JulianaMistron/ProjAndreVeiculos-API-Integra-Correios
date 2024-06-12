using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ProjAPICarro.Data;
using Service;

namespace ProjAPICarro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly ProjAPICarroContext _context;
        private readonly ICorreiosService _correiosService;

        public FuncionariosController(ProjAPICarroContext context)
        {
            _context = context;
            _correiosService = new CorreiosService();
        }

        // GET: api/Funcionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionario()
        {
            if (_context.Funcionario == null)
            {
                return NotFound();
            }
            return await _context.Funcionario.ToListAsync();
        }

        // GET: api/Funcionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(string id)
        {
            if (_context.Funcionario == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionario.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        // PUT: api/Funcionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(string id, Funcionario funcionario)
        {
            if (id != funcionario.Documento)
            {
                return BadRequest();
            }


            var result = _correiosService.ObterCepAsync(funcionario.Endereco.CEP);
            if (result != null)
            {
                funcionario.Endereco.Logradouro = result.Result.Logradouro;
                funcionario.Endereco.Bairro = result.Result.Bairro;
                funcionario.Endereco.Cidade = result.Result.Localidade;
                funcionario.Endereco.Uf = result.Result.Uf;

            }
            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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

        // POST: api/Funcionarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            if (_context.Funcionario == null)
            {
                return Problem("Entity set 'ProjAPICarroContext.Funcionario'  is null.");
            }
            var result = _correiosService.ObterCepAsync(funcionario.Endereco.CEP);
            if (result != null)
            {
                funcionario.Endereco.Logradouro = result.Result.Logradouro;
                funcionario.Endereco.Bairro = result.Result.Bairro;
                funcionario.Endereco.Cidade = result.Result.Localidade;
                funcionario.Endereco.Uf = result.Result.Uf;

            }
            _context.Funcionario.Add(funcionario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FuncionarioExists(funcionario.Documento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFuncionario", new { id = funcionario.Documento }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(string id)
        {
            if (_context.Funcionario == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuncionarioExists(string id)
        {
            return (_context.Funcionario?.Any(e => e.Documento == id)).GetValueOrDefault();
        }
    }
}
