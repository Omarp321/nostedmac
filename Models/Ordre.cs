using System;
namespace Nøsted.Models
{
    public class Ordre
    {
        public int OrdreId { get; set; }
        public DateTime Dato { get; set; } // Legg til feltet for Dato
        public string GodkjentAv { get; set; } // Legg til feltet for Godkjent av
        public string UtarbeidetAv { get; set; } // Legg til feltet for Utarbeidet av
        public string OrdreStatus { get; set; }
        public string OrdreInformasjon { get; set; }

        public Ordre()
        {

        }
    }
}
