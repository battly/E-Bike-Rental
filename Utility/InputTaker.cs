
//Diese Klasse wird benutzt, um die Eingaben des Benutzers bzgl. der Kunden, Kategorien und Fahrrädern richtig zu überprüfen.
public class InputTaker
{
    public static Customer CustomerInput()
    {
        string firstName, lastName, address;
        do
        {
            Console.WriteLine("Geben Sie den Vornamen des Kunden ein");
            firstName = Console.ReadLine()!;
            Console.WriteLine("Geben Sie den Nachnamen des Kunden ein");
            lastName = Console.ReadLine()!;

        } while (!Checker.CheckCustomerName(firstName, lastName));

        do
        {
            Console.WriteLine("Geben Sie die Adresse des Kunden ein");
            address = Console.ReadLine()!;
        }while(!Checker.CheckAddress(address));

        Console.WriteLine("Hat der Kunde eine AM-Lizenz? (true/false)");
        bool amlicence;

        try
        {
            amlicence = bool.Parse(Console.ReadLine()!);
        }
        catch (Exception)
        {
            Console.WriteLine("Die Eingabe des Boolean-Wertes ist nicht korrekt. Bitte geben Sie true oder false ein.");
            return CustomerInput();
        }

        Console.WriteLine("Geben Sie die IBAN des Kunden ein");
        string iban = Console.ReadLine()!;
        // Hier wird die IBAN geprüft und falls sie nicht korrekt ist, wird der Benutzer aufgefordert eine korrekte IBAN einzugeben.
        while (!Checker.CheckIBAN(iban))
        {
            if(iban == "Barzahlung")
            {
                break;
            }
            Console.WriteLine("Die IBAN ist nicht korrekt. Bitte geben Sie eine korrekte IBAN ein");
            iban = Console.ReadLine()!;
        }
        Customer customer = new Customer(firstName, lastName, address, amlicence, iban);
        return customer;
    }

    public static EBike_Category CategoryInput()
    {
        Console.WriteLine("Geben Sie den Namen der Kategorie ein");
        string name = Console.ReadLine()!.ToString();

        Console.WriteLine("Geben Sie den Preis pro Tag der Kategorie ein");
        double priceInDay = double.TryParse(Console.ReadLine(), out double price) ? price : 0;

        Console.WriteLine("Geben Sie die maximale Geschwindigkeit der Kategorie ein");
        int maxSpeed = int.TryParse(Console.ReadLine(), out int speed) ? speed : 0;

        if (priceInDay == 0 || maxSpeed == 0)
        {
            Console.WriteLine("Der Preis und/oder die maximale Geschwindigkeit ist nicht korrekt. Bitte geben Sie eine korrekte Zahl ein");
            return CategoryInput();
        }

        EBike_Category category = new EBike_Category(name, price, maxSpeed);

        return category;
    }

    public static EBike BikeInput()
    {
        Console.WriteLine("Geben Sie den Hersteller des E-Bikes ein: ");
        string name = Console.ReadLine()!.ToString();

        Console.WriteLine("Geben Sie das Modell des E-Bikes ein: ");
        string model = Console.ReadLine()!.ToString();

        Console.WriteLine("Geben Sie die Leistung des E-Bikes ein: ");

        double performance = double.TryParse(Console.ReadLine(), out double perf) ? perf : 0;

        if (performance == 0)
        {
            Console.WriteLine("Die Leistung ist nicht korrekt. Bitte wiederholen Sie die Eingabe.");
            return BikeInput();
        }

        EBike bike = new EBike(name, model, performance);
        return bike;
    }


    public static void BookingInput()
    {
        Console.WriteLine("Geben Sie den Vornamen des Kunden ein: ");
        string firstName = Console.ReadLine()!;

        Console.WriteLine("Geben Sie den Nachnamen des Kunden ein: ");
        string lastName = Console.ReadLine()!;

        Console.WriteLine("Geben Sie die Kategorie des zu buchenden E-Bikes ein:");
        string categoryInput = Console.ReadLine()!;

        Console.WriteLine("Geben Sie das Modell des zu buchenden E-Bike ein: ");
        string model = Console.ReadLine()!;

        Console.WriteLine("Geben Sie das Startdatum der Buchung ein: ");

        DateTime startDate = DateTime.TryParse(Console.ReadLine(), out DateTime firstDate) ? firstDate : new DateTime(0, 0, 0);

        Console.WriteLine("Geben Sie das Enddatum der Buchung ein: ");
        DateTime endDate = DateTime.TryParse(Console.ReadLine(), out DateTime lastDate) ? lastDate : new DateTime(0, 0, 0);

        if (startDate == new DateTime(0, 0, 0) || endDate == new DateTime(0, 0, 0))
        {
            Console.WriteLine("Das Startdatum und/oder das Enddatum ist nicht korrekt. Bitte wiederholen Sie die Eingabe.");
            BookingInput();
        }

        var customers = Serializer.DeserializeCustomerList();
        var categories = Serializer.DeserializeCategories();

        var customer = customers.Find(c => c.FirstName == firstName && c.LastName == lastName);
        if (customer == null)
        {
            Console.WriteLine("Der Kunde ist nicht vorhanden. Bitte wiederholen Sie die Eingabe.");
            BookingInput();
        }

        var category = categories.Find(c => c.Name == categoryInput);

        if (category == null)
        {
            Console.WriteLine("Die Kategorie ist nicht vorhanden. Bitte wiederholen Sie die Eingabe.");
            BookingInput();
        }

        var bike = category!.Bikes.Find(b => b.Model == model);

        if (bike == null)
        {
            Console.WriteLine("Das E-Bike ist nicht vorhanden. Bitte wiederholen Sie die Eingabe.");
            BookingInput();
        }


        if (category.Bikes.Count == 0)
        {
            Console.WriteLine("Es sind keine E-Bikes in der Kategorie vorhanden. Bitte wiederholen Sie die Eingabe.");
            BookingInput();
        }


        Booking booking = new Booking(customer!, category, bike!, startDate, endDate);

        var bookings = Serializer.DeserializeBookingList();
        bookings.Add(booking);
        Serializer.SerializeBookingList(bookings);

    }
}