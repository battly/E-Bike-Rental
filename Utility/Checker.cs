
//Diese Klasse wurde erstellt, um die IBAN-Überprüfmethode zu enthalten.
public class Checker
{

    //Diese Methode überprüft, ob die IBAN eines Kunden gültig ist.
    public static bool CheckIBAN(string iban)
    {
        if (iban.Substring(0, 2) != "DE")
        {
            return false;
        }

        if (iban.Length != 22)
        {
            return false;
        }

        string IBAN = iban.Substring(4) + "131400";

        System.Numerics.BigInteger checkIBAN = System.Numerics.BigInteger.Parse(IBAN) % 97;

        checkIBAN = 98 - checkIBAN;

        if (checkIBAN != System.Numerics.BigInteger.Parse(iban.Substring(2, 2)))
        {

            return false;
        }

        return true;
    }



    //Diese Methode überprüft ob der Vorname und Nachname eines Kunden gültig sind.
    public static bool CheckCustomerName(string name, string lastName)
    {
        if ((name == null || lastName == null) || (name.Length < 2 || lastName.Length < 2)
        || (name.Length > 20 || lastName.Length > 20) || (!name.All(char.IsLetter) || !lastName.All(char.IsLetter)))
        {
            Console.WriteLine("Der Vor- und Nachname des Kunden sind ungültig. Bitte versuchen  Sie es nochmal: ");
            return false;
        }

        return true;
    }


    public static bool CheckAddress(string address)
    {
        if (address == null || address.Length < 5 || address.Length > 50)
        {
            return false;
        }
        return true;
    }
}



