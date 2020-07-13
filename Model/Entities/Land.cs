using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Land
    {
        //Properties
        public string ISOLandCode { get; set; }
        public string NISLandCode { get; set; }
        public string Naam { get; set; }
        public int AantalInwoners { get; set; }
        public float Oppervlakte { get; set; }
    }
}
