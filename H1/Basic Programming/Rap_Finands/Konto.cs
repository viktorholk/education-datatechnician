using System;
using System.Collections.Generic;
using System.Linq;
namespace Rap_Finands {
    class Konto {

        /*
        fed metode til at lave helt nye kontonumre ~Konrad
        */
        private string LavEtKontoNummer()
        {
            Random tilfael = new Random();
            string nr = tilfael.Next(1, 9).ToString();
            for (var i = 1; i <= 9; i++)
            {
                nr = nr + tilfael.Next(0, 9).ToString();
                if (i == 3) nr = nr + " ";
                if (i == 6) nr = nr + " ";
            }
            return nr;
        }

        public static List<Konto> Konti = new List<Konto>();
        public static Konto OpretKonto()
        {
            Console.Write("Navn på kontoejer:");
            string ejerInput =  Console.ReadLine();
            Konto k = new Konto(ejerInput);
            Console.WriteLine("Konto oprettet!");
            Program.Gem();
            return k;
        }

        public static Konto FindKonto()
        {
            for (var i = 0; i <= Konti.Count - 1; i++)
            {
                Konto konto = Konti[i];
                Console.WriteLine($"{i + 1} . {konto.RegistreringsNr} {konto.Kontonr} ejes af {konto.Ejer}");
            }
            Console.WriteLine("Vælg et tal fra 1 til " + Konti.Count);
            Console.Write(">");
            int tal = int.Parse(Console.ReadLine());
            if (tal < 1 || tal > Konti.Count)
            {
                Console.WriteLine("Ugyldigt valg");
                Console.Clear();
                return null;
            }
            return Konti[tal - 1];
        }

        public string RegistreringsNr { get; set; }
        public string Kontonr   { get; set; }
        public string Ejer      { get; set; }
        public List<Transaktion> Transaktioner = new List<Transaktion>();
        public Konto(string ejer) {
            this.Ejer = ejer;
            this.RegistreringsNr = Program.Reginummer; //Sæt registreringsnummer på kontoen!
            this.Kontonr = LavEtKontoNummer(); //Lav et nyt (tilfældigt shh!) kontonummer

            // Gem til listen over alle konti
            Konti.Add(this);
        }

        public float FindSaldo()
        {
            if (Transaktioner.Count > 0)
            {
                Transaktion seneste = Transaktioner[^1];
                return seneste.Saldo;
            }
            return 0;
        }
        public void OpretTransaktion()
        {
            Console.Write("Tekst: ");
            string tekst = Console.ReadLine();
            Console.Write("Beløb: ");
            float amount = Program.GetInputFloat();
            if (GemTrans(tekst, amount))
            {
                Console.WriteLine("Transkationen blev gemt. Ny saldo på kontoen: " + FindSaldo());
                Program.Gem();
            }
            else
                Console.WriteLine("Transaktionen kunne ikke gemmes (Der var sikkert ikke penge nok på kontoen)");
        }
        public bool GemTrans(string tekst, float amount)
        {
            var saldo = FindSaldo();
            if (saldo + amount < 0) return false;
            var t = new Transaktion(tekst, amount);
            t.Saldo = t.Amount + saldo;
            Transaktioner.Add(t);
            return true;
        }

        public void Udskriv()
        {
            Console.WriteLine("Konto for " + Ejer + ": " + RegistreringsNr + " " + Kontonr);
            Console.WriteLine("================");
            Console.WriteLine("Tekst\t\t\t\tBeløb\t\tSaldo");
            foreach (Transaktion t in Transaktioner)
            {
                Console.Write(t.Tekst + "\t\t\t\t");
                Console.Write(t.Amount + "\t\t");
                Console.WriteLine(t.Saldo);
            }
            Console.WriteLine("================\n");
        }

    }
}
/** 
Koden er lavet til undervisningbrug på TECHCOLLEGE
Voldum Bank og nævnte personer er fiktive.
~Simon Hoxer Bønding
**/