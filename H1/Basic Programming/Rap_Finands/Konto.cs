using System;
using System.Collections.Generic;
namespace Rap_Finands {
    class Konto {
        public string RegistreringsNr { get; set; }
        public string Kontonr   { get; set; }
        public string Ejer      { get; set; }
        public List<Transaktion> Transaktioner = new List<Transaktion>();
        public Konto() {
            this.RegistreringsNr = Program.reginummer; //Sæt registreringsnummer på kontoen!
            this.Kontonr = Program.LavEtKontoNummer(); //Lav et nyt (tilfældigt shh!) kontonummer
        }
        
    }
}
/** 
Koden er lavet til undervisningbrug på TECHCOLLEGE
Voldum Bank og nævnte personer er fiktive.
~Simon Hoxer Bønding
**/