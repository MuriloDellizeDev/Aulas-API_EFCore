using EF_Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_Core.Interfaces
{
    public interface IPedidoRepository
    {

        List<Pedido> Listar();
        Pedido BuscarPorId(Guid id);
        Pedido Adicionar(List<PedidoItem> pedidosItens);
    }
}
