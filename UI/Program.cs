using Model.Entities;
using Model.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Model.Services;

namespace UI
{
    class Program
    {
        private static readonly LandService service = new LandService();

        static void Main(string[] args)
        {
            Hoofdmenu();
        }

        static void Hoofdmenu()
        {
            string[] input = { "A", "B", "C", "D", "E", "F", "G", "H", "X" };
            var keuze = "";
            while (keuze != "X")
            {
                Console.WriteLine();
                Console.WriteLine("=================");
                Console.WriteLine("H O O F D M E N U");
                Console.WriteLine($"=================");
                Console.WriteLine("A: Toon alle landen");
                Console.WriteLine("B: Toon gegevens van een land");
                Console.WriteLine("C: Toon steden in een land");
                Console.WriteLine("D: Toon de landstalen van een land");
                Console.WriteLine("E: Wijzig aantal inwoners van een land");
                Console.WriteLine("F: Wijzig oppervlakte van een land");
                Console.WriteLine("G: Voeg stad toe aan een land");
                Console.WriteLine("H: Verwijder stad van een land");
                Console.WriteLine();
                Console.Write("Geef uw keuze: ");
                keuze = Console.ReadLine().ToUpper();
                Console.WriteLine();
                while (!input.Contains(keuze))
                {
                    Console.Write("Geef uw keuze: ");
                    keuze = Console.ReadLine().ToUpper();
                }
                switch (keuze)
                {
                    case "A":
                        ToonAlleLanden();
                        break;
                    case "B":
                        ToonEenLand();
                        break;
                    case "C":
                        ToonLandMetSteden();
                        break;
                    case "D":
                        ToonLandMetTalen();
                        break;
                    case "E":
                        AantalInwonersWijzigen();
                        break;
                    case "F":
                        OppervlakteWijzigen();
                        break;
                    case "G":
                        StedenToevoegen();
                        break;
                    case "H":
                        StedenVerwijderen();
                        break;
                }
            }
        }

        static void ToonAlleLanden()
        {
            Console.WriteLine("Lijst van landen");
            Console.WriteLine("----------------");

            foreach (var land in service.FindAlleLanden())
            {
                Console.WriteLine($"{land.ISOLandCode} {land.Naam}  {land.AantalInwoners}   {land.Oppervlakte}");
            }

            Console.WriteLine();
        }

        static void ToonLandMetTalen()
        {
            Console.WriteLine("Lijst van talen van een land");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();
            var result = service.FindEenLandMetTalen(landCode);
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

        static void ToonLandMetSteden()
        {
            Console.WriteLine("Lijst van steden in land");
            Console.WriteLine("------------------------");
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();
            var land = service.FindEenLandMetSteden(landCode);

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

        static void ToonEenLand()
        {
            Console.WriteLine("Gegevens van een land");
            Console.WriteLine("---------------------");
            Console.WriteLine("Geef een landcode in: ");

            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();
            var land = service.FindEenLand(landCode);

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

        static void OppervlakteWijzigen()
        {
            Console.WriteLine("Wijzig oppervlakte");
            Console.WriteLine("--------------");
            Console.WriteLine("Geef een landcode in: ");
            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();

            var geselecteerdLand = service.FindEenLand(landCode);

            if (geselecteerdLand != null)
            {
                Console.WriteLine($"Land: {geselecteerdLand.Naam}");
                Console.WriteLine($"Huidige oppervlakte: {geselecteerdLand.Oppervlakte}");
                Console.WriteLine("Nieuwe oppervlakte:");

                if (float.TryParse(Console.ReadLine(), out float nieuweOppervlakte))
                {
                    service.WijzigOppervlakte(landCode, nieuweOppervlakte);
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

        static void AantalInwonersWijzigen()
        {
            Console.WriteLine("Wijzig inwoners");
            Console.WriteLine("--------------");
            Console.WriteLine("Geef een landcode in: ");
            var landCode = Console.ReadLine().ToUpper();
            Console.WriteLine();

            var geselecteerdLand = service.FindEenLand(landCode);

            if (geselecteerdLand != null)
            {
                Console.WriteLine($"Land: {geselecteerdLand.Naam}");
                Console.WriteLine($"Huidig aantal inwoners: {geselecteerdLand.AantalInwoners}");
                Console.WriteLine("Nieuw aantal inwoners:");

                if (int.TryParse(Console.ReadLine(), out int nieuwAantalInwoners))
                {
                    service.WijzigInwoners(landCode, nieuwAantalInwoners);
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

        static void StedenToevoegen()
        {
            Console.WriteLine("Stad toevoegen");
            Console.WriteLine("--------------");
            Console.WriteLine("Geef een landcode in: ");
            var landCode = Console.ReadLine().ToUpper();

            if (service.FindEenLand(landCode) != null)
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
                    service.StadToevoegen(stad);
                }
                catch (Exception)
                {

                    Console.WriteLine("Naam is te lang");
                }
            }
            else
            {
                Console.WriteLine("Land niet gevonden");
            }
            Console.WriteLine();
        }

        static void StedenVerwijderen()
        {
            Console.WriteLine("Stad verwijderen");
            Console.WriteLine("----------------");
            Console.WriteLine("Welke stad wil je verwijderen?");
            var naamStad = Console.ReadLine().ToUpper();
            Console.WriteLine("Wat is de landcode van deze stad?");
            var landCodeStad = Console.ReadLine().ToUpper();

            try
            {
                service.StadVerwijderen(naamStad, landCodeStad);
            }
            catch (Exception)
            {

                Console.WriteLine("Stad niet gevonden");
            }
            Console.WriteLine();
        }
    }
}
