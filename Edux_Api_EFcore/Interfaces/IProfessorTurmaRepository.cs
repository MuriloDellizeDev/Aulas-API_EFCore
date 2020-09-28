using Edux_Api_EFcore.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux_Api_EFcore.Interfaces
{
    interface IProfessorTurmaRepository
    {
        List<ProfessorTurma> Buscar();

        ProfessorTurma Buscar(Guid id);

        void CadastrarRelacao(ProfessorTurma professorTurma);

        void EditarRelacao(ProfessorTurma professorTurma);

        void ExcluirRelacao(Guid id);
    }
}