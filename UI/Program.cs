using Model.Entities;
using Model.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace UI
{
    class Program
    {
        private static readonly EFTestContext context = new EFTestContext();

        static void Main(string[] args)
        {
            ToonAlleLanden();
            ToonEenLand();
            ToonLandMetSteden();
        }

        static void ToonAlleLanden()
        {
            Console.WriteLine("Lijst van landen");
            Console.WriteLine("----------------");

            foreach (var land in FindAlleLanden())
            {
                Console.WriteLine($"{land.ISOLandCode} {land.Naam}  {land.AantalInwoners}   {land.Oppervlakte}");
            }

            Console.WriteLine();
        }

        static void ToonEenLand()
        {
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine();
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

        static void ToonLandMetSteden()
        {
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine();
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

        static List<Land> FindAlleLanden()
        {
            using var context = new EFTestContext();

            var landen = (from land in context.Landen
                          orderby land.Naam
                          select land).ToList();

            return landen;
        }

        static Land FindEenLand(string landCode)
        {
            using var context = new EFTestContext();

            var land = context.Landen.Find(landCode);

            return land;
        }

        static Land FindEenLandMetSteden(string landCode)
        {
            using var context = new EFTestContext();

            var landMetSteden = (from land in context.Landen.Include("Steden")
                                   where land.ISOLandCode == landCode
                                   select land).FirstOrDefault();

            return landMetSteden;
        }
    }
}
