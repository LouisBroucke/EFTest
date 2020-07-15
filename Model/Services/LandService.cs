using Model.Entities;
using Model.Repositories;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Model.Services
{
    public class LandService
    {
        public List<Land> FindAlleLanden()
        {
            using var context = new EFTestContext();

            var landen = (from land in context.Landen
                          orderby land.Naam
                          select land).ToList();

            return landen;
        }

        public Land FindEenLand(string landCode)
        {
            using var context = new EFTestContext();

            var land = context.Landen.Find(landCode);

            return land;
        }

        public Land FindEenLandMetSteden(string landCode)
        {
            using var context = new EFTestContext();

            var landMetSteden = (from land in context.Landen.Include("Steden")
                                 where land.ISOLandCode == landCode
                                 select land).FirstOrDefault();

            return landMetSteden;
        }

        public Tuple<List<LandTaal>, Land> FindEenLandMetTalen(string landCode)
        {
            using var context = new EFTestContext();

            var landMetTalen = (from landsTalen in context.LandsTalen.Include("Taal")
                                where landsTalen.ISOLandCode == landCode
                                select landsTalen).ToList();

            var geselecteerdLand = context.Landen.Find(landCode);

            return new Tuple<List<LandTaal>, Land>(landMetTalen, geselecteerdLand);
        }                

        public void WijzigInwoners(string landCode, int aantalInwoners)
        {
            using var context = new EFTestContext();

            var land = context.Landen.Find(landCode);
            land.AantalInwoners = aantalInwoners;
            context.SaveChanges();
        }

        public void WijzigOppervlakte(string landCode, float oppervlakte)
        {
            using var context = new EFTestContext();

            var land = context.Landen.Find(landCode);
            land.Oppervlakte = oppervlakte;
            context.SaveChanges();
        }

        public void StadToevoegen(Stad stad)
        {
            using var context = new EFTestContext();

            context.Steden.Add(stad);
            context.SaveChanges();
        }

        public void StadVerwijderen(string naamStad, string landCodeStad)
        {
            using var context = new EFTestContext();

            var teVerwijderenStad = (from stad in context.Steden
                        where stad.Naam == naamStad && stad.ISOLandCode == landCodeStad
                        select stad).FirstOrDefault();

            context.Steden.Remove(teVerwijderenStad);
            context.SaveChanges();
        }
    }
}
