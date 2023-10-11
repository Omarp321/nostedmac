using System;
namespace Nøsted.Models
{
    public class Ordre
    {
        public int OrdreId { get; set; }
        public string? OrdreStatus { get; set; }
        public string OrdreInformasjon { get; set; }

        public Ordre()
        {

        }
    }
}
