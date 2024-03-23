using System.Xml.Serialization;
public class Booking 
{
    
    [XmlElement("Guid")]
    public Guid Guid { get; set; }

    [XmlElement("Customer")]
    public Customer BookingCustomer{ get; set; }

    [XmlElement("Category")]
    public EBike_Category BookingCategory { get; set; }

    [XmlElement("Bike")]
    public EBike BookingBike { get; set; }

    [XmlElement("BookingStart")]
    public DateTime BookingStart { get; set; }

    [XmlElement("BookingEnd")]
    public DateTime BookingEnd { get; set; }

    [XmlElement("TotalCost")]
    public double TotalCost { get; set; }

    public Booking(Customer customer, EBike_Category category, EBike bike, DateTime start, DateTime end)
    {
        Guid = Guid.NewGuid();
        BookingCustomer = customer;
        BookingCategory = category;
        BookingBike = bike;
        BookingStart = start;
        BookingEnd = end;
        TotalCost = CalculateTotalCost();
    }

    public Booking(Guid guid, Customer customer, EBike_Category category, EBike bike, DateTime start, DateTime end, double totalCost)
    {
        Guid = guid;
        BookingCustomer = customer;
        BookingCategory = category;
        BookingBike = bike;
        BookingStart = start;
        BookingEnd = end;
        TotalCost = totalCost;
    }


    //die Methode ermittelt die Gesamtkosten der jeweiligen Buchung
    public double CalculateTotalCost()
    {
        double totalCost = 0;
        TimeSpan timeSpan = BookingEnd - BookingStart;
        int days = timeSpan.Days;
        totalCost = days * BookingCategory.RentalPricePerDay;
        return totalCost;
    }


    //Diese Methode gibt alle Buchungen aus.
    

    //Diese Methode gibt einen String zurück, der alle Informationen der Buchung enthält.
    public override string ToString()
    {
        return BookingCustomer.FirstName + " " + BookingCustomer.LastName + " / " + BookingCategory.Name + " / " + 
        BookingBike.Producer + " " + BookingBike.Model + " / " + BookingStart.ToString().Substring(0, 10) + " | " 
        + BookingEnd.ToString().Substring(0, 10) + " / " + TotalCost;
    }


}