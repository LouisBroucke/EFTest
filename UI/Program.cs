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
                        service.ToonAlleLanden();
                        break;
                    case "B":
                        service.ToonEenLand();
                        break;
                    case "C":
                        service.ToonLandMetSteden();
                        break;
                    case "D":
                        service.ToonLandMetTalen();
                        break;
                    case "E":
                        service.WijzigInwoners();
                        break;
                    case "F":
                        service.WijzigOppervlakte();
                        break;
                    case "G":
                        service.StadToevoegen();
                        break;
                    case "H":
                        service.StadVerwijderen();
                        break;
                }
            }
        }
    }
}
