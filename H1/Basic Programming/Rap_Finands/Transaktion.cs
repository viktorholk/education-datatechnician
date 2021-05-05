using System;

namespace Rap_Finands {
    class Transaktion {
        public string Tekst { get; set; }
        public float Saldo { get; set; }
        public float Amount { get; set; }
        public DateTime Dato { get; set; }

        public Transaktion(string tekst, float amount)
        {
            this.Tekst = tekst;
            this.Amount = amount;
            this.Dato = DateTime.Now;
        }
    }
}
/** 
Koden er lavet til undervisningbrug på TECHCOLLEGE
Voldum Bank og nævnte personer er fiktive.
~Simon Hoxer Bønding
**/