using EF_Core.Domains;
using System;
using System.Collections.Generic;

namespace EF_Core.Interfaces
{
    interface IProdutoRepository
    {

        List<Produto> Listar();

        Produto BuscarPorId( Guid Id);

        List<Produto> BuscarPorNome(string nome);

        void Adicionar(Produto produto);

        void Editar(Produto produto);

        void Remover(Guid Id);

    }
}
