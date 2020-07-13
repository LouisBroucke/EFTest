using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class LandTaal
    {
        //Properties
        public string LandCode { get; set; }
        public string TaalCode { get; set; }

        //Navigation Properties
        public virtual Land Land { get; set; }
        public virtual Taal Taal { get; set; }
    }
}
