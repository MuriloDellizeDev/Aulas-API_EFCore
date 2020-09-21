using System;
using System.Collections.Generic;

namespace Nyous_Api_EFcore.Domains
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int? IdAcesso { get; set; }

        public virtual Acesso IdAcessoNavigation { get; set; }
    }
}
