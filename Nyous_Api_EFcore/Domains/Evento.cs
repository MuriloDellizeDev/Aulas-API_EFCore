using System;
using System.Collections.Generic;

namespace Nyous_Api_EFcore.Domains
{
    public partial class Evento
    {
        public int IdEvento { get; set; }
        public DateTime DataDoEvento { get; set; }
        public byte[] AcessoRestrito { get; set; }
        public int Capacidade { get; set; }
        public string Descricao { get; set; }
        public int? IdLocalizacao { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Localizacao IdLocalizacaoNavigation { get; set; }
    }
}
