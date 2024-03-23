public class Program {
    public static void Main(string[] args) {

        //bestehende Daten werden geladen
        ObjectManager.bookings = Serializer.DeserializeBookingList();
        ObjectManager.categories = Serializer.DeserializeCategories();
        ObjectManager.customers = Serializer.DeserializeCustomerList();

        //das hier ist die Methode, die das Programm startet
        StartProgram();
    }


    //Das ist der Eingangspunkt für das Programm.
    public static void StartProgram()
    {
        MainMenu();
    }


    //Das ist das Hauptmenü, von dem aus auf die anderen Menüs zugegriffen werden kann.
    public static void MainMenu()
    {

        Console.WriteLine("Willkommen im E-Bike Verleih! \n");


        Console.WriteLine("Bitte wählen Sie eine Funktion aus: (die Nummer eingeben)");

        Console.WriteLine("1. E-Bike Verwaltung");
        Console.WriteLine("2. Kundenverwaltung");
        Console.WriteLine("3. Buchungsverwaltung");
        Console.WriteLine("4. Beenden");

        Console.Write("\nIhre Wahl: ");

        string choice = Console.ReadLine()!;
        Console.WriteLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                BikeMenu();
                break;
            case "2":
                CustomerMenu();
                break;
            case "3":
                BookingMenu();
                break;
            case "4":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Falsche Eingabe");
                MainMenu();
                break;
        }  

        Console.WriteLine();
        Console.WriteLine("________________");
        Console.WriteLine();

        MainMenu();
    }

    //Diese Methode steuert die Erstell-, Lösch-, und Bearbeitungsfunktionen der Kategorien, und beinhaltet zusätzlich den 
    //Zugriff auf die E-Bike Funktionen. 
    //
    public static void BikeMenu()
    {
        ObjectManager.categories = Serializer.DeserializeCategories();
        Console.WriteLine("Sie haben sich für die E-Bike Verwaltung entschieden. \n");
        

        //die Kategorien werden auf der Konsole ausgegeben
        Console.WriteLine("Die verfügbaren Kategorien mit den jeweiligen E-Bikes: \n");
        ObjectManager.PrintCategories();

        Console.WriteLine("1. E-Bike Kategorie hinzufügen");
        Console.WriteLine("2. E-Bike Kategorie löschen");
        Console.WriteLine("3. E-Bike Kategorie bearbeiten");

        //Auf die Fahrradfunktionen wird zugegriffen, wenn eine Kategorie ausgewählt wird.
        Console.WriteLine("4. E-Bike Funktionen");
        Console.WriteLine("5. Zurück");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                ObjectManager.CreateCategory();
                break;
            case "2":
                ObjectManager.DeleteCategory();
                break;
            case "3":
                ObjectManager.UpdateCategory();
                break;
            case "4":
                BikeFunctions();
                break;
            case "5":
                MainMenu();
                break;
            default:
                Console.WriteLine("Falsche Eingabe");
                BikeMenu();
                break;
        }
        //nach den Funktionen werden die Daten in die XML-Datei geschrieben. Da die Dateien geladen und in die Listen geschrieben werden, 
        //kann die XML-Datei überschrieben werden, da die alten Daten in der Liste gespeichert werden.
        Serializer.SerializeCategories(ObjectManager.categories);
        
        
    }

    //Dieses Menü steuert die Erstell-, Lösch-, und Bearbeitungsfunktionen der E-Bikes.
    public static void BikeFunctions()
    {
        
        Console.WriteLine("E-Bike Funktionen: \n");

        
        EBike_Category category = ObjectManager.FindCategory();

        ObjectManager.PrintCategory(category);
        Console.WriteLine("\n");


        Console.WriteLine("1. E-Bike hinzufügen");
        Console.WriteLine("2. E-Bike löschen");
        Console.WriteLine("3. E-Bike bearbeiten");
        Console.WriteLine("4. Zurück");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                category.CreateBike();
                break;
            case "2":
                category.DeleteBike();
                break;
            case "3":
                category.UpdateBike();
                break;
            case "4":
                BikeMenu();
                break;
            default:
                Console.WriteLine("Falsche Eingabe");
                BikeFunctions();
                break;
        }
        Serializer.SerializeCategories(ObjectManager.categories);
    }

    // Dieses Menü steuert die Kundenverwaltung. Hier kann man entweder einen Kunden hinzufügen, löschen oder bearbeiten.
    public static void CustomerMenu()
    {
        ObjectManager.customers = Serializer.DeserializeCustomerList();

        Console.WriteLine("Kundenverwaltung:");

        ObjectManager.PrintCustomers();

        Console.WriteLine("1. Kunde hinzufügen");
        Console.WriteLine("2. Kunde löschen");
        Console.WriteLine("3. Kunde bearbeiten");
        Console.WriteLine("4. Zurück");

        Console.Write("Ihre Wahl: ");

        string choice = Console.ReadLine()!;

        Console.WriteLine();

        switch (choice)
        {
            case "1":
                ObjectManager.CreateCustomer();
                break;
            case "2":
                ObjectManager.DeleteCustomer();
                break;
            case "3":
                ObjectManager.UpdateCustomer();
                break;
            case "4":
                MainMenu();
                break;
            default:
                Console.WriteLine("Falsche Eingabe");
                CustomerMenu();
                break;
        }

        Serializer.SerializeCustomerList(ObjectManager.customers);
    }

    //Dieses Menü beinhaltet alle Buchung-bezogenen Funktionen wie Buchung hinzufügen, löschen oder bearbeiten.
    public static void BookingMenu()
    {
        ObjectManager.bookings = Serializer.DeserializeBookingList();

        Console.WriteLine("Buchungsverwaltung: \n");

        ObjectManager.PrintBookings();
        Console.WriteLine();

        Console.WriteLine("1. Buchung hinzufügen");
        Console.WriteLine("2. Buchung löschen");
        Console.WriteLine("3. Buchung bearbeiten");
        Console.WriteLine("4. Zurück");

        string choice = Console.ReadLine()!;

        switch (choice)
        {

            case "1":
                ObjectManager.CreateBooking();
                break;
            case "2":
                ObjectManager.DeleteBooking();
                break;
            case "3":
                ObjectManager.UpdateBooking();
                break;
            case "4":
                MainMenu();
                break;
            default:
                Console.WriteLine("Falsche Eingabe");
                BookingMenu();
                break;
        }

        //Die neue Serialisierungsmethode
        Serializer.SerializeBookingList(ObjectManager.bookings);
    }
}
