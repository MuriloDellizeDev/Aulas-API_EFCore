using System;
using System.ComponentModel.DataAnnotations;

namespace EF_Core.Domains
{
    public class Pedido : BaseDomains
    {
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
