using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Rap_Finands
{
    /**
    Dette BANK PROGRAM ER LAVET af Konrad Sommer! Copy(c) Right All rights reserveret 2020
    idé og udtænkt af Anne Dam for Voldum Bank I/S
    Rap Finands
    **/
    class Program
    {
        public static string Reginummer = "4242";
        public static string Datafil = "bank.json"; //Her ligger alt data i
        static void Main(string[] args)
        {
            Console.WriteLine("Henter alt kontodata");
            
            Hent();
            if (Konto.Konti.Count == 0) {
                var k = new Konto("Ejvind Møller");

                k.GemTrans("Opsparing",100);
                k.GemTrans("Vandt i klasselotteriet",1000);
                k.GemTrans("Hævet til Petuniaer",-50);
                
                Gem();
            }

            Start();
            
        }
        static void Start() {
            Console.WriteLine("Velkommen til Rap Finans af Konrad Sommer");
            Console.WriteLine("Hvad vil du gøre nu?");
            
            bool blivVedOgVed = true;
            while (blivVedOgVed) {
                Console.WriteLine("1. Opret ny konto");
                Console.WriteLine("2. Hæv/sæt ind");
                Console.WriteLine("3. Se en oversigt");
                Console.WriteLine("0. Afslut");

                Console.Write(">");
                int valg = int.Parse(Console.ReadLine());
                
                switch (valg) {
                    case 1:
                        Konto.OpretKonto();
                        break;
                    case 2:
                        Konto.FindKonto().OpretTransaktion();
                        break;
                    case 3:
                        Konto.FindKonto().Udskriv();
                        break;
                    case 0:
                        blivVedOgVed = false;
                        break;
                    default:
                        Console.WriteLine("UGYLDIGT VALGT!!");
                        Console.ReadKey();
                        break;

                }
            }
            Console.Clear();
        }
        public static void Gem() 
        {
            File.WriteAllText(Datafil,JsonConvert.SerializeObject(Konto.Konti));
        }
        public static void Hent()
        {
            if (File.Exists(Datafil)) {
                string json = File.ReadAllText(Datafil);
                Konto.Konti = JsonConvert.DeserializeObject<List<Konto>>(json);
            } else {
                Konto.Konti = new List<Konto>();
            }
        }
    }
}
/** 
Koden er lavet til undervisningbrug på TECHCOLLEGE
Voldum Bank og nævnte personer er fiktive.
~Simon Hoxer Bønding
**/
