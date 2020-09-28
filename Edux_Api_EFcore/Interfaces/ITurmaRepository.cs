using Edux_Api_EFcore.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux_Api_EFcore.Interfaces
{
    interface ITurmaRepository
    {
        List<Turma> Listar();

        Turma BuscarPorId(Guid Id);

        List<Turma> BuscarPorNome(string nome);

        Turma Adicionar(Turma turma);

        void Editar(Turma turma);

        void Remover(Guid Id);
    }
}
