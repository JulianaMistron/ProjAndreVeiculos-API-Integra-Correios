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
    public class ClientesController : ControllerBase
    {
        private readonly ProjAPICarroContext _context;
        private readonly ICorreiosService _correiosService;

        public ClientesController(ProjAPICarroContext context)
        {
            _context = context;
            _correiosService = new CorreiosService();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(string id, Cliente cliente)
        {
            if (id != cliente.Documento)
            {
                return BadRequest();
            }

            var result = _correiosService.ObterCepAsync(cliente.Endereco.CEP);
            if (result != null)
            {
                cliente.Endereco.Logradouro = result.Result.Logradouro;
                cliente.Endereco.Bairro = result.Result.Bairro;
                cliente.Endereco.Cidade = result.Result.Localidade;
                cliente.Endereco.Uf = result.Result.Uf;

            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'ProjAPICarroContext.Cliente'  is null.");
            }

            var result = _correiosService.ObterCepAsync(cliente.Endereco.CEP);
            if (result != null)
            {
                cliente.Endereco.Logradouro = result.Result.Logradouro;
                cliente.Endereco.Bairro = result.Result.Bairro;
                cliente.Endereco.Cidade = result.Result.Localidade;
                cliente.Endereco.Uf = result.Result.Uf;

            }

            _context.Cliente.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.Documento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.Documento }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string id)
        {
            return (_context.Cliente?.Any(e => e.Documento == id)).GetValueOrDefault();
        }
    }
}
