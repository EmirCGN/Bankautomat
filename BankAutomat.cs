using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;

namespace Automat
{
    public class BankAutomat
    {
        private List<KartenInhaber> kartenInhaberList;
        private string jsonFilePath;

        public BankAutomat(string jsonFilePath)
        {
            this.jsonFilePath = jsonFilePath;
            LoadKartenInhaber();
        }

        private void LoadKartenInhaber()
        {
            if (!File.Exists(jsonFilePath))
            {
                kartenInhaberList = new List<KartenInhaber>();
                SaveKartenInhaber();
            }
            else
            {
                string json = File.ReadAllText(jsonFilePath);
                kartenInhaberList = JsonConvert.DeserializeObject<List<KartenInhaber>>(json);
            }
        }

        private void SaveKartenInhaber()
        {
            string json = JsonConvert.SerializeObject(kartenInhaberList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        public void Start()
        {
            Console.WriteLine("Willkommen am Bankautomaten");
            Console.WriteLine("Bitte stecken Sie Ihre Karte ein");
            string debitKartenNum = "";
            string pinNum = "";
            KartenInhaber currentUser = null;

            while (currentUser == null)
            {
                debitKartenNum = Console.ReadLine();
                currentUser = kartenInhaberList.FirstOrDefault(a => a.KartenNum == debitKartenNum);

                if (currentUser == null)
                {
                    Console.WriteLine("Karte wurde nicht erkannt. Möchten Sie ein neues Konto erstellen? (ja/nein)");
                    string createNewAccount = Console.ReadLine();

                    if (createNewAccount.ToLower() == "ja")
                    {
                        currentUser = ErstelleNeuesKonto(debitKartenNum);
                        kartenInhaberList.Add(currentUser);
                        SaveKartenInhaber();
                        Console.WriteLine("Neues Konto erstellt. Bitte geben Sie Ihre Daten ein:");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Bitte versuchen Sie es erneut!");
                    }
                }
            }

            Console.WriteLine("Willkommen " + currentUser.Vorname + "!");

            int option = 0;
            do
            {
                PrintOptionen();
                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch { }

                switch (option)
                {
                    case 1:
                        Einzahlen(currentUser);
                        break;
                    case 2:
                        Abheben(currentUser);
                        break;
                    case 3:
                        KontostandAnzeigen(currentUser);
                        break;
                    case 4:
                        break;
                    default:
                        option = 0;
                        break;
                }
            } while (option != 4);

            SaveKartenInhaber();

            Console.WriteLine("Auf Wiedersehen " + currentUser.Vorname + " " + currentUser.Nachname + "!");
        }

        private void PrintOptionen()
        {
            Console.WriteLine("Bitte wählen Sie eine der folgenden Optionen aus:");
            Console.WriteLine("1. Einzahlen");
            Console.WriteLine("2. Abheben");
            Console.WriteLine("3. Kontostand anzeigen");
            Console.WriteLine("4. Exit");
        }

        private KartenInhaber ErstelleNeuesKonto(string kartenNummer)
        {
            Console.WriteLine("Bitte geben Sie Ihre persönlichen Daten ein:");
            Console.Write("Vorname: ");
            string vorname = Console.ReadLine();
            Console.Write("Nachname: ");
            string nachname = Console.ReadLine();
            Console.Write("PIN: ");
            int pin = int.Parse(Console.ReadLine());
            double kontostand = 0.0;

            return new KartenInhaber(kartenNummer, pin, vorname, nachname, kontostand);
        }

        private void Einzahlen(KartenInhaber currentUser)
        {
            Console.WriteLine("Wie viel Euro möchten Sie einzahlen?");
            double einzahlen = double.Parse(Console.ReadLine());
            currentUser.Kontostand += einzahlen;
            Console.WriteLine("Die Einzahlung war erfolgreich! Kontostand: " + currentUser.Kontostand + "\n");
        }

        private void Abheben(KartenInhaber currentUser)
        {
            Console.WriteLine("Wie viel Euro möchten Sie von Ihrem Konto abheben?");
            double abheben = double.Parse(Console.ReadLine());

            if (currentUser.Kontostand >= abheben)
            {
                currentUser.Kontostand -= abheben;
                Console.WriteLine("Sie haben erfolgreich Geld abgehoben!\n");
            }
            else
            {
                Console.WriteLine("Unzureichender Kontostand!");
            }
        }

        private void KontostandAnzeigen(KartenInhaber currentUser)
        {
            Console.WriteLine("Aktueller Kontostand: " + currentUser.Kontostand + "\n");
        }
    }
}
