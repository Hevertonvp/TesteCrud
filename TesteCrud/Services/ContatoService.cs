using TesteCrud.Data;
using TesteCrud.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TesteCrud.Models.Dto;
using System;

namespace TesteCrud.Services
{
    public class ContatoService
    {
        TesteCrudContext _context;

        public ContatoService(TesteCrudContext context)
        {
            _context = context;
        }
        public async Task<List<Contato>> FindAllAsync()
        {
            return await _context.Contato.ToListAsync();
        }

        public async Task InsertAsync(Contato contato)
        {
            _context.Add(contato);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contato contato)
        {
            if (!_context.Contato.Any(x => x.Id == contato.Id))
            {
                throw new System.Exception("Código não encontrado!");
            }
            try
            {
                _context.Update(contato);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);

            }
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Contato.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Contato> FindById(int id)
        {
            return await _context.Contato.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<ContatoResumoDto>> FiltrarPorCidade()
        {
            var dados = await _context.Contato
                                           .GroupBy(obj => new { obj.Cidade, obj.Data.Month, obj.Sexo })
                                           .Select(x => new ContatoResumoDto
                                           {
                                               Cidade = x.Key.Cidade,
                                               Mes = x.Key.Month.ToString(),
                                               TotalContatos = x.Count(),
                                               TotalPorSexo = x.Select(a => x.Key.Sexo).Count(),
                                               Sexo = x.Key.Sexo
                                           })
                                           .OrderBy(x => x.Cidade)
                                           .ToListAsync();

            return dados;
        }


    }

}


