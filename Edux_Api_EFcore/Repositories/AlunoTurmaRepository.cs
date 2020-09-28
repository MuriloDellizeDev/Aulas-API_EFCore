using Edux_Api_EFcore.Contexts;
using Edux_Api_EFcore.Domains;
using Edux_Api_EFcore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux_Api_EFcore.Repositories
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly EduxContext _ctx;

        public AlunoTurmaRepository()
        {
            _ctx = new EduxContext();
        }

        public List<AlunoTurma> Buscar()
        {
            try
            {
                return _ctx.AlunosTurmas
                    .Include(a => a.Turma)
                    .ThenInclude(a => a.AlunosTurmas)
                    .ThenInclude(a  => a.Usuario)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AlunoTurma Buscar(Guid id)
        {
            try
            {
                return _ctx.AlunosTurmas
                    .Include(a => a.Turma)
                    .ThenInclude(a => a.AlunosTurmas)
                    .ThenInclude(a => a.Usuario)
                    .FirstOrDefault(a => a.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CadastrarRelacao(AlunoTurma alunoTurma)
        {
            try
            {
                _ctx.AlunosTurmas.Add(alunoTurma);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditarRelacao(AlunoTurma alunoTurma)
        {
            try
            {
                AlunoTurma alunoTurmaEditada = Buscar(alunoTurma.Id);

                if (alunoTurmaEditada == null)
                    throw new Exception("Impossível incluir a edição da relação pois faltam dados.");

                _ctx.AlunosTurmas.Update(alunoTurmaEditada);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ExcluirRelacao(Guid id)
        {
            try
            {
                AlunoTurma alunoTurmaASerExcluido = Buscar(id);

                if (alunoTurmaASerExcluido == null)
                    throw new Exception("Impossível excluir a relação pois faltam dados.");

                _ctx.AlunosTurmas.Remove(alunoTurmaASerExcluido);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}