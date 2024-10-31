using Fattocs.data;
using Fattocs.Models;
using Microsoft.EntityFrameworkCore;

namespace Fattocs.services
{
    public class ListaTarefasServices
    {
        private readonly AppDbContext _context;

        public ListaTarefasServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListaTarefas>> GetAllAsync() => await _context.ListaTarefas.ToListAsync();

        public async Task<ListaTarefas> GetByIdAsync(int id) => await _context.ListaTarefas.FindAsync(id);

        public async Task<(bool Success, string Message, ListaTarefas? Tarefa)> AddAsync(ListaTarefas tarefa)
        {
            // Verifica duplicidade com o mesmo Nome
            bool duplicidade = _context.ListaTarefas.Any(t => t.Nome == tarefa.Nome);
            if (duplicidade)
            {
                return (false, "Já existe uma tarefa com o mesmo nome.", null); // Retorna mensagem de erro para duplicidade
            }

            // Adiciona a tarefa ao contexto e salva
            await _context.ListaTarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();

            // Define o valor de Ordenacao como Id + 1 após salvar o Id
            tarefa.Ordenacao = tarefa.Id + 1;
            _context.ListaTarefas.Update(tarefa);
            await _context.SaveChangesAsync();

            // Retorna sucesso com a tarefa atualizada
            return (true, "Tarefa adicionada com sucesso.", tarefa);
        }

        public async Task DeleteById(int id)
        {
            await _context.ListaTarefas
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();
        }



        public async Task<(bool Success, string Message, ListaTarefas? Tarefa)> UpdateTarefa(int id, ListaTarefas TarefaAtualizada)
        {
            // Localiza a tarefa existente
            var Tar = await _context.ListaTarefas.FindAsync(id);
            if (Tar == null)
            {
                return (false, "Tarefa não encontrada.", null); // Retorna mensagem de erro se a tarefa não for encontrada
            }

            // Verifica duplicidade com o mesmo Nome
            bool duplicidade = _context.ListaTarefas.Any(t => t.Nome == TarefaAtualizada.Nome && t.Id != id);
            if (duplicidade)
            {
                return (false, "Já existe uma tarefa com o mesmo nome.", null); // Retorna mensagem de erro para duplicidade
            }

            // Atualiza as propriedades da tarefa existente
            Tar.Nome = TarefaAtualizada.Nome;
            Tar.Custo = TarefaAtualizada.Custo;
            Tar.DataLimite = TarefaAtualizada.DataLimite;
            // ... defina outras propriedades conforme necessário

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna sucesso com a tarefa atualizada
            return (true, "Tarefa atualizada com sucesso.", Tar);
        }

    }

}
