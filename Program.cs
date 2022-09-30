public class kartenInhaber
{
    string kartennum;
    int pin;
    string vorname;
    string nachname;
    double kontostand;

    public kartenInhaber(string kartennum, int pin, string vorname, string nachname, double kontostand)
    {
        this.kartennum = kartennum;
        this.pin = pin;
        this.vorname = vorname;
        this.nachname = nachname;
        this.kontostand = kontostand;
    }

    public String getNum()
    {
        return kartennum;
    }
    public int getPin()
    {
        return pin;
    }
    public string getVorname()
    {
        return vorname;
    }
    public string getNachname()
    {
        return nachname;
    }
    public double getKontostand()
    {
        return kontostand;
    }

    public void setNum(String newKartennum)
    {
        kartennum = newKartennum;
    }

    public void setPin(int newPin)
    {
        pin = newPin;
    }
    public void setVorname(String newVorname)
    {
        vorname = vorname;
    }
    public void setNachname(String newNachname)
    {
        nachname = newNachname;
    }
    public void setKontostand(double newKontostand)
    {
        kontostand = newKontostand;
    }

    public static void Main(String[] args)
    {
        void printOptionen()
        {
            Console.WriteLine("Bitte wähle einer der Folgenden Optionen aus!");
            Console.WriteLine("1. Einzahlen");
            Console.WriteLine("2. Abheben");
            Console.WriteLine("3. Kontostand anzeigen");
            Console.WriteLine("4. Exit");
        }
        void einzahlen(kartenInhaber currentUser)
        {
            Console.WriteLine("Wie viel Euro möchten Sie einzahlen?");
            double einzahlen = Double.Parse(Console.ReadLine());
            currentUser.setKontostand(einzahlen);
            Console.WriteLine("Die Einzahlung war Erfolgreich! Ihr neuer Kontostand ist: " + currentUser.getKontostand());
        }

        void abheben(kartenInhaber currentUser)
        {
            Console.WriteLine("Wie viel Euro möchten Sie von ihr Konto abheben?");
            double abheben = Double.Parse(Console.ReadLine());
            //überprüft ob der Kontoinhaber genügend Geld hat auf sein Konto
            if(currentUser.getKontostand() > abheben)
            {
                Console.WriteLine("unzureichender Kontostand!");
            }
            else
            {
                currentUser.setKontostand(currentUser.getKontostand() - abheben);
                Console.WriteLine("Sie haben Erfolgreich Geld abgehoben!\n");
            }
        }

        void kontostand(kartenInhaber currentUser)
        {
            Console.WriteLine("Aktueller Kontostand: " + currentUser.getKontostand()+"\n");
        }

        List<kartenInhaber> kartenInhaber = new List<kartenInhaber>();
        kartenInhaber.Add(new kartenInhaber("68500105178297336485", 1234, "Caillou", "Musterman", 150.31));
        kartenInhaber.Add(new kartenInhaber("8506195138297956485", 2341, "Manuellsen", "Abi", 9999999999999999.31));

        //Begrüßt den Nutzer 
        Console.WriteLine("Willkommen am Bankautomaten");
        Console.WriteLine("Bitte stecken Sie Ihre Karte ein");
        String debitKartenNum = "";
        String pinNum = "";
        kartenInhaber currentUser;

        while (true)
        {
            try
            {
                debitKartenNum = Console.ReadLine();
                currentUser = kartenInhaber.FirstOrDefault(a => a.kartennum == debitKartenNum);
                if(currentUser != null) { break;
                }
                else
                {
                    Console.WriteLine("Karte wurde nicht erkannt. Bitte versuchen Sie es erneut!");
                }
            }
            catch {
                Console.WriteLine("Karte wurde nicht erkannt. Bitte versuchen Sie es erneut!");
            }
            Console.WriteLine("Bitte geben Sie ihren Pin an!");
            int userPin = 0;
            while (true){
                try{
                    userPin = int.Parse(Console.ReadLine());
                    currentUser = kartenInhaber.FirstOrDefault(a => a.kartennum == debitKartenNum);
                    if(currentUser.getPin() == userPin) { break; }
                    else { Console.WriteLine("Karte wurde nicht erkannt. Bitte versuchen Sie es erneut!"); }
                }catch
                {Console.WriteLine("Karte wurde nicht erkannt. Bitte versuchen Sie es erneut!");}
            }
        }
        Console.WriteLine("Willkommen " + currentUser.getVorname() + "!");
        int option = 0;
        do
        {
            printOptionen();
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch { }
            if (option == 2) { einzahlen(currentUser); }
            else if (option == 2) { abheben(currentUser); }
            else if (option == 3) { kontostand(currentUser); }
            else if (option == 4) { break; }
            else { option = 0; }
        }
        while (option != 4);
        Console.WriteLine("Auf Wiedersehen " + currentUser.getVorname() + " " + currentUser.getNachname() + "!");
    }
}