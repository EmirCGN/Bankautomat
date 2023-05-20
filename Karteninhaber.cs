using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Automat
{
    public class KartenInhaber
    {
        public string KartenNum { get; set; }
        public int Pin { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public double Kontostand { get; set; }

        public KartenInhaber(string kartenNum, int pin, string vorname, string nachname, double kontostand)
        {
            KartenNum = kartenNum;
            Pin = pin;
            Vorname = vorname;
            Nachname = nachname;
            Kontostand = kontostand;
        }
    }
}
