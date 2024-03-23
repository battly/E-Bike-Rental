using System.Xml.Serialization;
using System.Numerics;

[XmlRoot("Customer")]
public class Customer
{
    [XmlElement("Guid")]
    private Guid guid;
    public Guid Guid
    {
        get { return guid; }
        set { guid = value; }
    }
    [XmlElement("FirstName")]
    public string FirstName { get; set; }
    [XmlElement("LastName")]
    public string LastName { get; set; }
    [XmlElement("Address")]
    public string Address { get; set; }
    [XmlElement("AMLicence")]
    public bool AMLicence { get; set; }

    [XmlElement("IBAN")]
    public string IBAN { get; set; }


    //you have to use the checkIBAN method of the Checker class to set the IBAN




    // Das ist der Konstruktor, der für das Deserialisieren der Kundenliste verwendet wird
    public Customer(Guid guid, string firstName, string lastName, string address, bool amlicence, string iban)
    {
        Guid = guid;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        AMLicence = amlicence;
        IBAN = iban;
    }

    //Das ist der Konstruktor, der für die Erstellung eines neuen Kunden verwendet wird. 
    public Customer(string firstName, string lastName, string address, bool amlicence, string iban)
    {
        Guid = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        AMLicence = amlicence;
        IBAN = iban;
    }

    //Dieser Konstruktor wird genutzt, um das Deserialisieren der Buchungsliste vorzunehmen.
    public Customer(Guid customerGuid, string customerFirstName, string customerLastName, string customerAddress)
    {
        Guid = customerGuid;
        FirstName = customerFirstName;
        LastName = customerLastName;
        Address = customerAddress;
        AMLicence = false;
        IBAN = "00000000";
    }

    public override string ToString()
    {
        return $"{FirstName} / {LastName} / {Address} / {AMLicence} / {IBAN}";
    }
}

