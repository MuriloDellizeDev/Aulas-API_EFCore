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
    public class ProfessorTurmaRepository : IProfessorTurmaRepository
    {
        private readonly EduxContext _ctx;

        public ProfessorTurmaRepository()
        {
            _ctx = new EduxContext();
        }

        public List<ProfessorTurma> Buscar()
        {
            try
            {
                return _ctx.ProfessoresTurmas
                    .Include(p => p.Turma)
                    .ThenInclude(p => p.ProfessoresTurmas)
                    .ThenInclude(p => p.Usuario)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProfessorTurma Buscar(Guid id)
        {
            try
            {
                return _ctx.ProfessoresTurmas
                    .Include(p => p.Turma)
                    .ThenInclude(p => p.ProfessoresTurmas)
                    .ThenInclude(p => p.Usuario)
                    .FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CadastrarRelacao(ProfessorTurma professorTurma)
        {
            try
            {
                _ctx.ProfessoresTurmas.Add(professorTurma);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditarRelacao(ProfessorTurma professorTurma)
        {
            try
            {
                ProfessorTurma professorTurmaEditado = Buscar(professorTurma.Id);

                if (professorTurmaEditado == null)
                    throw new Exception("Impossível incluir a edição da relação pois faltam dados.");

                _ctx.ProfessoresTurmas.Update(professorTurmaEditado);
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
                ProfessorTurma professorTurmaASerExcluido = Buscar(id);

                if (professorTurmaASerExcluido == null)
                    throw new Exception("Impossível excluir a relação pois faltam dados.");

                _ctx.ProfessoresTurmas.Remove(professorTurmaASerExcluido);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}