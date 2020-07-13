using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Stad
    {
        //Properties
        public int StadId { get; set; }
        public string Naam { get; set; }
        public string ISOLandCode { get; set; }

        //Navigation Properties
        public virtual Land Land { get; set; }
    }
}
