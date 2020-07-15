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
        public void ToonAlleLanden()
        {
            Console.WriteLine("Lijst van landen");
            Console.WriteLine("----------------");

            foreach (var land in FindAlleLanden())
            {
                Console.WriteLine($"{land.ISOLandCode} {land.Naam}  {land.AantalInwoners}   {land.Oppervlakte}");
            }

            Console.WriteLine();
        }

        public void ToonEenLand()
        {
            Console.WriteLine("Gegevens van een land");
            Console.WriteLine("---------------------");
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();
            var land = FindEenLand(landCode);

            if (land != null)
            {
                Console.WriteLine($"Gegevens {land.Naam}: ");
                Console.WriteLine($"Aantal inwoners: {land.AantalInwoners}");
                Console.WriteLine($"Oppervlakte: {land.Oppervlakte}");
            }
            else
            {
                Console.WriteLine("Land niet gevonden");
            }
            Console.WriteLine();
        }

        public void ToonLandMetSteden()
        {
            Console.WriteLine("Lijst van steden in land");
            Console.WriteLine("------------------------");
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();
            var land = FindEenLandMetSteden(landCode);

            if (land != null)
            {
                Console.WriteLine($"Steden {land.Naam}: ");

                foreach (var stad in land.Steden)
                {
                    Console.WriteLine(stad.Naam);
                }
            }
            else
            {
                Console.WriteLine("Land niet gevonden");
            }
            Console.WriteLine();
        }

        public void ToonLandMetTalen()
        {
            Console.WriteLine("Lijst van talen van een land");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();
            var result = FindEenLandMetTalen(landCode);
            var land = result.Item2;
            var talen = result.Item1;

            if (land != null)
            {
                Console.WriteLine($"Talen {land.Naam}:");

                foreach (var landTaal in land.LandsTalen)
                {
                    Console.WriteLine(landTaal.Taal.NaamNL);
                }
            }
            else
            {
                Console.WriteLine("Land niet gevonden");
            }
            Console.WriteLine();
        }

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

        public void StadToevoegen()
        {
            using var context = new EFTestContext();

            Console.WriteLine("Stad toevoegen");
            Console.WriteLine("--------------");
            Console.WriteLine("Geef een landcode in: ");
            var landCode = Console.ReadLine().ToUpper();

            if (FindEenLand(landCode) != null)
            {
                Console.WriteLine("Welke stad wil je toevoegen?");
                var naam = Console.ReadLine();

                var stad = new Stad
                {
                    ISOLandCode = landCode,
                    Naam = naam
                };

                try
                {
                    context.Steden.Add(stad);
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    Console.WriteLine("Naam is te lang"); ;
                }
            }
            else
            {
                Console.WriteLine("Land niet gevonden");
            }
            Console.WriteLine();
        }

        public void StadVerwijderen()
        {
            using var context = new EFTestContext();

            Console.WriteLine("Stad verwijderen");
            Console.WriteLine("----------------");
            Console.WriteLine("Welke stad wil je verwijderen?");
            var naamStad = Console.ReadLine().ToUpper();
            Console.WriteLine("Wat is de landcode van deze stad?");
            var landCodeStad = Console.ReadLine().ToUpper();

            var teVerwijderenStad = (from stad in context.Steden
                                     where stad.Naam == naamStad && stad.ISOLandCode == landCodeStad
                                     select stad).FirstOrDefault();

            if (teVerwijderenStad != null)
            {
                context.Remove(teVerwijderenStad);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Stad niet gevonden");
            }
            Console.WriteLine();
        }

        public void WijzigInwoners()
        {
            using var context = new EFTestContext();

            Console.WriteLine("Wijzig inwoners");
            Console.WriteLine("--------------");
            Console.WriteLine("Geef een landcode in: ");
            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();

            var geselecteerdLand = context.Landen.Find(landCode);

            if (geselecteerdLand != null)
            {
                Console.WriteLine($"Land: {geselecteerdLand.Naam}");
                Console.WriteLine($"Huidig aantal inwoners: {geselecteerdLand.AantalInwoners}");
                Console.WriteLine("Nieuw aantal inwoners:");

                if (int.TryParse(Console.ReadLine(), out int nieuwAantalInwoners))
                {
                    geselecteerdLand.AantalInwoners = nieuwAantalInwoners;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Tik een getal!");
                }
            }
            else
            {
                Console.WriteLine("Land niet gevonden");
            }
        }

        public void WijzigOppervlakte()
        {
            using var context = new EFTestContext();

            Console.WriteLine("Wijzig oppervlakte");
            Console.WriteLine("--------------");
            Console.WriteLine("Geef een landcode in: ");
            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();

            var geselecteerdLand = context.Landen.Find(landCode);

            if (geselecteerdLand != null)
            {
                Console.WriteLine($"Land: {geselecteerdLand.Naam}");
                Console.WriteLine($"Huidige oppervlakte: {geselecteerdLand.Oppervlakte}");
                Console.WriteLine("Nieuwe oppervlakte:");

                if (float.TryParse(Console.ReadLine(), out float nieuweOppervlakte))
                {
                    geselecteerdLand.Oppervlakte = nieuweOppervlakte;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Tik een getal!");
                }
            }
            else
            {
                Console.WriteLine("Land niet gevonden");
            }
        }
    }
}
