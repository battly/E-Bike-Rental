public class ObjectManager
{
    //this is the list that saves
    public static List<Booking> bookings = new List<Booking>();

    public static List<Customer> customers = new List<Customer>();

    public static List<EBike_Category> categories = new List<EBike_Category>();

     public static void CreateCustomer()
    {
        Customer customer = InputTaker.CustomerInput();
        //customers = Serializer.DeserializeCustomerList();
        customers.Add(customer);
        Serializer.SerializeCustomerList(customers);
    }

    public static void DeleteCustomer()
    {
        Console.Write("Geben Sie den Vornamen des Kunden ein, den Sie löschen möchten: ");
        string firstName = Console.ReadLine()!;

        Console.WriteLine("\nGeben Sie den Nachnamen des Kunden ein, den Sie löschen möchten:");
        string lastName = Console.ReadLine()!;

        Console.WriteLine();
        if (Checker.CheckCustomerName(firstName, lastName))
        {

            //customers = Serializer.DeserializeCustomerList();
            foreach (Customer customer in customers)
            {
                if (customer.FirstName == firstName && customer.LastName == lastName)
                {
                    customers.Remove(customer);
                    break;
                }

                if (customer == customers[customers.Count - 1])
                {
                    Console.WriteLine("Der Kunde wurde nicht gefunden, bitte wiederholen Sie die Eingabe.");
                    DeleteCustomer();
                }
            }
        }

        else
        {
            Console.WriteLine("Der eingegebene Vor- und/oder Nachname sind ungültig.");
            DeleteCustomer();
        }
        Serializer.SerializeCustomerList(customers);
    }

    public static void UpdateCustomer()
    {
        Console.Write("Bitte geben Sie den Vornamen des Kunden, den Sie bearbeiten möchten, ein: ");
        string firstName = Console.ReadLine()!;
        Console.Write("\nBitte geben Sie den Nachnamen des Kunde, den Sie bearbeiten möchten, ein: ");
        string lastName = Console.ReadLine()!;

        if (Checker.CheckCustomerName(firstName, lastName))
        {

            Console.WriteLine();
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].FirstName == firstName && customers[i].LastName == lastName)
                {
                    customers[i] = InputTaker.CustomerInput();
                    break;
                }

                if (i == customers.Count - 1)
                {
                    Console.WriteLine("Der Kunde wurde nicht gefunden, bitte wiederholen Sie die Einabe.");
                    UpdateCustomer();
                }

            }
        }
        else
        {
            Console.WriteLine("Der eingegebene Vor- und/oder Nachname sind nicht gültig.");
        }


        Serializer.SerializeCustomerList(customers);
    }

    public static Customer FindCustomer()
    {
        bool found = false;

        do
        {
            Console.Write("Geben Sie den Vornamen des Kunden ein: ");
            string firstName = Console.ReadLine()!;
            Console.WriteLine("\nGeben Sie den Nachnamen des Kunden ein:");
            string lastName = Console.ReadLine()!;
            Console.WriteLine();
            foreach (Customer customer in customers)
            {
                if (customer.FirstName == firstName && customer.LastName == lastName)
                {
                    found = true;
                    return customer;
                }
            }

            Console.WriteLine("Bitte wiederholen Sie die Eingabe. Der Kunde wurde nicht gefunden.");
        } while (!found);

        //dieser Exception wird nie geworfen.
        throw new Exception("Error: Der Kunde wurde nicht gefunden.");

    }


    public static void CreateCategory()
    {
        EBike_Category category = InputTaker.CategoryInput();
        categories.Add(category);
    }

    public static void DeleteCategory()
    {
        Console.WriteLine("Geben Sie den Namen der Kategorie ein, die Sie löschen möchten: ");
        string name = Console.ReadLine()!;
        foreach (EBike_Category category in categories)
        {
            if (category.Name == name)
            {
                categories.Remove(category);
                break;
            }
        }
    }

    public static void UpdateCategory()
    {
        EBike_Category category = FindCategory();
        var otherCategory = InputTaker.CategoryInput();
        if (category != null)
        {
            category.Name = otherCategory.Name;
            category.RentalPricePerDay = otherCategory.RentalPricePerDay;
            category.MaxSpeed = otherCategory.MaxSpeed;


            Console.WriteLine($"Änderungen vorgenommen: {category.ToString()}");
            
        }
        else
        {
            Console.WriteLine("Kategorie nicht vorhanden.");
        }
    }
    // Wenn die Kategorie nicht gefunden wird, wird eine Exception geworfen.
    public static EBike_Category FindCategory()
    {
        //categories = Serializer.DeserializeCategories();
        do
        {
            Console.WriteLine("Geben Sie den Namen der Kategorie ein: ");
            string name = Console.ReadLine()!;
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == name)
                {
                    return categories[i];
                }
            }
            Console.WriteLine("Kategorie nicht vorhanden.");
        } while (true);


        //Es wird immer ein Wert zurückgegeben, daher wird diese Exception nie geworfen.
        throw new Exception("Error: Kategorie nicht vorhanden.");
    }


    //this method creates a booking
    public static void CreateBooking()
    {
        Console.WriteLine("Wählen Sie einen Kunden aus: ");

        PrintCustomers();
        Console.WriteLine();
        var customer = FindCustomer();
        EBike_Category category;
        PrintCategories();
        Console.WriteLine();
        bool loop = true;
        do
        {
            category = FindCategory();

            if(category.MaxSpeed > 25 && !customer.AMLicence)
            {
                Console.WriteLine("Der Kunde hat keinen AM-Führerschein und kann diese Kategorie nicht mieten");
                continue;
            }
            else
            {
                loop = false;
            }
        } 
        while(loop);
        
        Console.WriteLine("Wählen Sie ein Fahrrad aus:");
        //category.PrintBikes();
        var bike = category.FindBike();

        Console.WriteLine("Geben Sie das Startdatum ein:");
        DateTime start = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("Geben Sie das Enddatum ein:");
        DateTime end = DateTime.Parse(Console.ReadLine()!);

        Booking booking = new Booking(customer, category, bike, start, end);
        bookings.Add(booking);
    }

    //this method deletes a booking
    public static void DeleteBooking()
    {
        Customer customer = FindCustomer();
        EBike_Category category = FindCategory();
        EBike bike = category.FindBike();

        foreach (Booking booking in bookings)
        {
            if (booking.BookingCustomer.FirstName == customer.FirstName && booking.BookingCustomer.LastName == customer.LastName 
            && booking.BookingCategory.Name == category.Name && booking.BookingBike.Model == bike.Model)
            {
                bookings.Remove(booking);
                break;
            }

            if(booking == bookings[bookings.Count - 1])
            {
                Console.WriteLine("Keine Buchung gefunden");
            }
        }
    }

    //this method updates a booking
    public static void UpdateBooking()
    {
        Customer customer = FindCustomer();
        EBike_Category category = FindCategory();
        EBike bike = category.FindBike();

        foreach (Booking booking in bookings)
        {
            if (booking.BookingCustomer.FirstName == customer.FirstName && booking.BookingCustomer.LastName == customer.LastName
             && booking.BookingCategory.Name == category.Name && booking.BookingBike.Model == bike.Model)
            {
                Console.WriteLine("Wählen Sie einen neuen Kunden aus: ");

                PrintCustomers();
                var newCustomer = FindCustomer();

                
                Console.WriteLine("Wählen Sie eine neue Kategorie aus:");
                PrintCategories();


                EBike_Category newCategory; //= EBike_Category.FindCategory();

                bool loop = true;
                do
                {
                    newCategory = FindCategory();

                    if (newCategory.MaxSpeed > 25 && !newCustomer.AMLicence)
                    {
                        Console.WriteLine("Der Kunde hat keinen AM-Führerschein und kann diese Kategorie nicht mieten");
                        continue;
                    }
                    else
                    {
                        loop = false;
                    }
                }while(loop);

                Console.WriteLine("Wählen Sie ein neues Bike aus:");
                newCategory.PrintBikes();
                var newBike = newCategory.FindBike();

                Console.WriteLine("Geben Sie ein neues Startdatum ein");
                DateTime newStart = DateTime.Parse(Console.ReadLine()!);

                Console.WriteLine("Geben Sie ein neues Enddatum ein");
                DateTime newEnd = DateTime.Parse(Console.ReadLine()!);

                booking.BookingCustomer = newCustomer;
                booking.BookingCategory = newCategory;
                booking.BookingBike = newBike;
                booking.BookingStart = newStart;
                booking.BookingEnd = newEnd;
                booking.TotalCost = booking.CalculateTotalCost();
                break; 
            }
            if(booking == bookings[bookings.Count - 1])
                {
                    Console.WriteLine();
                    Console.WriteLine("Es konnte eine Buchung mit den gegebenen Daten gefunden werden. ");
                }
        }
    }



    public static void PrintCustomers()
    {
        foreach (Customer customer in customers)
        {
            Console.WriteLine();
            Console.WriteLine("Vorname / Nachname / Adresse / AM-Lizenz / IBAN");
            Console.WriteLine(customer.ToString());
            Console.WriteLine("----------------------------------------");
        }
        Console.WriteLine();
    }

    

    public static void PrintCategories()
    {
        foreach (EBike_Category category in categories)
        {
            PrintCategory(category);
        }


    }
    public static void PrintCategory(EBike_Category category)
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Kategorie: \n\tName: " + category.Name + " \n\tPreis Pro Tag: " +
        category.RentalPricePerDay + " \n\tMaximale Geschwindigkeit: " + category.MaxSpeed);
        Console.WriteLine();
        Console.Write("E-Bikes (Produzent / Model / Performanz):\n");
        category.PrintBikes();
        Console.WriteLine("-------------------------------------");
    }

    public static void PrintBookings()
    {
        Console.WriteLine("Vorname Nachname / Kategorie / Produzent Model/ Startdatum | Enddatum / Gesamtkosten \n");
        foreach (Booking booking in bookings)
        {
            Console.WriteLine(booking.ToString());
            Console.WriteLine("--------------------------------------------------");
        }
    }
}