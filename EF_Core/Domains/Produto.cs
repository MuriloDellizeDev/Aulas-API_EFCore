using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_Core.Domains
{

    //Define a classe do produto
    public class Produto : BaseDomains
    {
     
        public string Nome { get; set; }
        public float Preco { get; set; }

        //Relacionamento com a tabela pedido item 1,N
        public List<PedidoItem> PedidosItens { get; set; }


    }

}
