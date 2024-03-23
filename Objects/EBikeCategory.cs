using System.Xml.Serialization;

public class EBike_Category
{


    private Guid guid;
    public Guid Guid { get; set; }

    [XmlElement("CategoryName")]
    public string Name { get; set; }

    [XmlElement("PricePerDay")]
    private double rentalPricePerDay;
    public double RentalPricePerDay
    {
        get { return rentalPricePerDay; }
        set { rentalPricePerDay = value; }
    }


    //Das Attribut, Preis Pro Woche wird intern aus dem Preis pro Tag berechnet und gespeichert.
    [XmlElement("PricePerWeek")]
    public double RentalPricePerWeek
    {
        get { return rentalPricePerDay * 7; }
        set { rentalPricePerDay = value / 7; }
    }

    [XmlElement("MaxSpeed")]
    public int MaxSpeed { get; set; }

    //Diese Liste beinhaltet alle Fahrräder, die zu dieser Kategorie gehören.
    [XmlElement("Bikes")]
    public List<EBike> Bikes { get; set; }



    public EBike_Category(string name, double rentalPricePerDay, int maxSpeed)
    {
        guid = Guid.NewGuid();
        Name = name;
        RentalPricePerDay = rentalPricePerDay;
        MaxSpeed = maxSpeed;
        Bikes = new List<EBike>();
    }

    public EBike_Category(Guid guid, string name, double rentalPricePerDay, double rentalPricePerWeek, int maxSpeed, List<EBike> bikes)
    {
        this.guid = guid;
        Name = name;
        RentalPricePerDay = rentalPricePerDay;
        RentalPricePerWeek = rentalPricePerWeek;
        MaxSpeed = maxSpeed;
        Bikes = bikes;
    }

    //dieser Konstruktor wird für das Deserialisieren der Buchungsliste verwendet. Das es lediglich ein Platzhalter ist
    //sind die Felder hier allle ausgefüllt.
    public EBike_Category(Guid guid, string name, double rentalPricePerDay, double rentalPricePerWeek, int maxSpeed)
    {
        this.guid = guid;
        Name = name;
        RentalPricePerDay = rentalPricePerDay;
        RentalPricePerWeek = rentalPricePerWeek;
        MaxSpeed = maxSpeed;
        Bikes = new List<EBike>();
    }

    

    public void CreateBike()
    {
        EBike bike = InputTaker.BikeInput();
        this.Bikes.Add(bike);
        Serializer.SerializeCategories(ObjectManager.categories);
    }

    //create a method that deletes bikes from the list
    public void DeleteBike()
    {
        Console.Write("Bitte geben Sie den Namen des E-Bikes ein, das Sie löschen möchten: ");
        string name = Console.ReadLine()!;
        Console.WriteLine();
        foreach (EBike bike in this.Bikes)
        {
            if (bike.Model == name)
            {
                this.Bikes.Remove(bike);
                break;
            }
        }
        Serializer.SerializeCategories(ObjectManager.categories);
    }

    public void UpdateBike()
    {
        EBike bike = FindBike();
        var otherBike = InputTaker.BikeInput();
        if (bike != null)
        {
            bike.Producer = otherBike.Producer;
            bike.Model = otherBike.Model;
            bike.Performance = otherBike.Performance;

            Console.WriteLine(bike.ToString());
            Serializer.SerializeCategories(ObjectManager.categories);
        }
        else
        {
            Console.WriteLine("Bike not found");
        }
    }

    //Diese Methode finded ein Fahrrad mittels Modellname und gibt es zurück. 
    //Wenn es nicht gefunden wird, wird eine Exception geworfen.
    public EBike FindBike()
    {
        do
        {
            Console.WriteLine("Geben Sie den Namen des E-Bikes ein: ");
            string name = Console.ReadLine()!;
            for (int i = 0; i < this.Bikes.Count; i++)
            {
                if (this.Bikes[i].Model == name)
                {
                    return this.Bikes[i];
                }
            }
            
                Console.WriteLine("Fahrrad nicht vorhanden. Bitte versuchen Sie es erneut.");
            
        } while (true);
        //Dieser Code wird nie erreicht, da die Schleife immer einen Wert zurückgibt.
        throw new Exception("Error: Bike not found.");
    }

    //Es werden alle Kategorien mit den zugehürigen Fahrädern ausgegeben.
    

    //Diese Methode gibt eine Kategorie mit den zugehörigen Fahrrädern aus. Es wird innerhalb der PrintCategories() Methode verwendet.
    

    public void PrintBikes()
    {
        foreach (EBike bike in this.Bikes)
        {
            Console.WriteLine("\t" + bike.Producer + " / " + bike.Model + " / " + bike.Performance);
        }
    }


    public override string ToString()
    {
        return "Kategorie: " + Name + " " + RentalPricePerDay + " " + MaxSpeed;
    }
}


