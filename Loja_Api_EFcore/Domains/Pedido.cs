using System;
using System.Collections.Generic;

namespace EF_Core.Domains
{
    public class Pedido : BaseDomains
    {
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

        //Relacionamento com a tabela pedido item 1,N
        public List<PedidoItem> PedidosItens { get; set; }
    }
}
