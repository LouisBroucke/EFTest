using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Taal
    {
        //Properties
        public string ISOTaalCode { get; set; }
        public string NaamNL { get; set; }
        public string NaamTaal { get; set; }

        //Navigation Properties
        public virtual ICollection<LandTaal> TaalLanden { get; set; } = new List<LandTaal>();
    }
}
