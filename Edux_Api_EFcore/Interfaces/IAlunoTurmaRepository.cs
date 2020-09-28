using Edux_Api_EFcore.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux_Api_EFcore.Interfaces
{
    interface IAlunoTurmaRepository
    {
        List<AlunoTurma> Buscar();

        AlunoTurma Buscar(Guid id);

        void CadastrarRelacao(AlunoTurma alunoTurma);

        void EditarRelacao(AlunoTurma alunoTurma);

        void ExcluirRelacao(Guid id);
    }
}