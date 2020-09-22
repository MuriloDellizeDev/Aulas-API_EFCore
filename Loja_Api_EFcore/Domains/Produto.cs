using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EF_Core.Domains
{

    //Define a classe do produto
    public class Produto : BaseDomains
    {
     
        public string Nome { get; set; }
        public float Preco { get; set; }

        [NotMapped] // Não mapeia a propriedade no banco de dados
        [JsonIgnore] // Ignora propriedade no retorno do Json
        public IFormFile Imagem { get; set; }

        //Url da imagem do produto salva no servidor
        public string UrlImages { get; set; }

        //Relacionamento com a tabela pedido item 1,N
        public List<PedidoItem> PedidosItens { get; set; }


    }

}
