using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

public class Serializer
{

    static string bookingsFile = "bookings.xml";
    static string customersFile = "customers.xml";
    static string categoriesAndBikesFile = "categories.xml";
    //diese Methode speichert die Kundenliste in eine XML-Datei
    public static void SerializeCustomerList(List<Customer> customers)
    {
        XmlDocument doc = new XmlDocument();
        XmlNode rootNode = doc.CreateElement("Customers");
        doc.AppendChild(rootNode);
        foreach (Customer customer in customers)
        {
            XmlNode customerNode = doc.CreateElement("Customer");
            rootNode.AppendChild(customerNode);

            XmlNode guidNode = doc.CreateElement("Guid");
            guidNode.InnerText = customer.Guid.ToString();
            customerNode.AppendChild(guidNode);

            XmlNode firstNameNode = doc.CreateElement("FirstName");
            firstNameNode.InnerText = customer.FirstName;
            customerNode.AppendChild(firstNameNode);

            XmlNode lastNameNode = doc.CreateElement("LastName");
            lastNameNode.InnerText = customer.LastName;
            customerNode.AppendChild(lastNameNode);

            XmlNode addressNode = doc.CreateElement("Address");
            addressNode.InnerText = customer.Address;
            customerNode.AppendChild(addressNode);

            XmlNode amLicenceNode = doc.CreateElement("AMLicence");
            amLicenceNode.InnerText = customer.AMLicence.ToString();
            customerNode.AppendChild(amLicenceNode);

            XmlNode ibanNode = doc.CreateElement("IBAN");
            ibanNode.InnerText = customer.IBAN;
            customerNode.AppendChild(ibanNode);
        }
        doc.Save(customersFile);
    }


    //diese Methode deserialisiert die Kundenliste aus der XML-Datei und, fügt es der Kundenliste hinzu und gibt es zurück
    public static List<Customer> DeserializeCustomerList()
    {
        List<Customer> customers = new List<Customer>();
        XmlDocument doc = new XmlDocument();
        doc.Load(customersFile);

        XmlNode rootNode = doc.DocumentElement!;
        foreach (XmlNode customerNode in rootNode.ChildNodes)
        {
            Guid guid = Guid.Parse(customerNode.ChildNodes[0]!.InnerText);
            string firstName = customerNode.ChildNodes[1]!.InnerText;
            string lastName = customerNode.ChildNodes[2]!.InnerText;
            string address = customerNode.ChildNodes[3]!.InnerText;
            bool amLicence = bool.Parse(customerNode.ChildNodes[4]!.InnerText);
            string iban = customerNode.ChildNodes[5]!.InnerText;
            Customer customer = new Customer(guid, firstName, lastName, address, amLicence, iban);
            customers.Add(customer);
        }
        return customers;
    }

    //diese Methode speichert die Kategorienliste in eine XML-Datei
    public static void SerializeCategories(List<EBike_Category> categories)
    {

        XmlDocument doc = new XmlDocument();
        XmlNode rootNode = doc.CreateElement("Categories");
        doc.AppendChild(rootNode);
        foreach (EBike_Category category in categories)
        {
            XmlNode categoryNode = doc.CreateElement("E-Bike_Category");
            rootNode.AppendChild(categoryNode);

            XmlNode guidNode = doc.CreateElement("Guid");
            guidNode.InnerText = category.Guid.ToString();
            categoryNode.AppendChild(guidNode);

            XmlNode nameNode = doc.CreateElement("Name");
            nameNode.InnerText = category.Name;
            categoryNode.AppendChild(nameNode);

            XmlNode rentalPricePerDayNode = doc.CreateElement("RentalPricePerDay");
            rentalPricePerDayNode.InnerText = category.RentalPricePerDay.ToString();
            categoryNode.AppendChild(rentalPricePerDayNode);

            XmlNode rentalPricePerWeekNode = doc.CreateElement("RentalPricePerWeek");
            rentalPricePerWeekNode.InnerText = category.RentalPricePerWeek.ToString();
            categoryNode.AppendChild(rentalPricePerWeekNode);

            XmlNode maxSpeedNode = doc.CreateElement("MaxSpeed");
            maxSpeedNode.InnerText = category.MaxSpeed.ToString();
            categoryNode.AppendChild(maxSpeedNode);

            XmlNode bikesNode = doc.CreateElement("Bikes");
            categoryNode.AppendChild(bikesNode);

            foreach (EBike bike in category.Bikes)
            {
                XmlNode bikeNode = doc.CreateElement("Bike");
                bikesNode.AppendChild(bikeNode);

                XmlNode bikeGuidNode = doc.CreateElement("Guid");
                bikeGuidNode.InnerText = bike.Guid.ToString();
                bikeNode.AppendChild(bikeGuidNode);

                XmlNode producerNode = doc.CreateElement("Producer");
                producerNode.InnerText = bike.Producer;
                bikeNode.AppendChild(producerNode);

                XmlNode modelNode = doc.CreateElement("Model");
                modelNode.InnerText = bike.Model;
                bikeNode.AppendChild(modelNode);

                XmlNode performanceNode = doc.CreateElement("Performance");
                performanceNode.InnerText = bike.Performance.ToString();
                bikeNode.AppendChild(performanceNode);
            }

        }
        doc.Save(categoriesAndBikesFile);
    }

    //Diese Methode deserialisiert die Kategorienliste aus der XML-Datei und gibt es zurück
    public static List<EBike_Category> DeserializeCategories()
    {
        List<EBike_Category> categories = new List<EBike_Category>();
        XmlDocument doc = new XmlDocument();
        doc.Load(categoriesAndBikesFile);
        XmlNode rootNode = doc.DocumentElement!;

        foreach (XmlNode categoryNode in rootNode.ChildNodes)
        {
            Guid guid = Guid.Parse(categoryNode.ChildNodes[0]!.InnerText);
            string name = categoryNode.ChildNodes[1]!.InnerText;
            double rentalPricePerDay = double.Parse(categoryNode.ChildNodes[2]!.InnerText);
            double rentalPricePerWeek = double.Parse(categoryNode.ChildNodes[3]!.InnerText);
            int maxSpeed = int.Parse(categoryNode.ChildNodes[4]!.InnerText);
            //fetch all bikes from the category
            List<EBike> bikes = new List<EBike>();
            foreach (XmlNode bikeNode in categoryNode.ChildNodes[5]!.ChildNodes)
            {
                Guid bikeGuid = Guid.Parse(bikeNode.ChildNodes[0]!.InnerText);
                string producer = bikeNode.ChildNodes[1]!.InnerText;
                string model = bikeNode.ChildNodes[2]!.InnerText;
                double performance = Double.Parse(bikeNode.ChildNodes[3]!.InnerText);
                //add a loop that deserializes each bike into the list
                EBike bike = new EBike(bikeGuid, producer, model, performance);
                bikes.Add(bike);
            }
            EBike_Category category = new EBike_Category(guid, name, rentalPricePerDay, rentalPricePerWeek, maxSpeed, bikes);
            categories.Add(category);
        }

        return categories;
    }




    //Das ist die Methode, die die Buchungsliste von der XML-Datei liest und als Liste zurückgibt
    public static List<Booking> DeserializeBookings()
    {
        List<Booking> bookings = new List<Booking>();
        XmlDocument doc = new XmlDocument();
        doc.Load(bookingsFile);
        XmlNode rootNode = doc.DocumentElement!;

        foreach (XmlNode bookingNode in rootNode.ChildNodes)
        {
            Guid guid = Guid.Parse(bookingNode.ChildNodes[0]!.InnerText);
            Guid customerGuid = Guid.Parse(bookingNode.ChildNodes[1]!.InnerText);
            Guid categoryGuid = Guid.Parse(bookingNode.ChildNodes[2]!.InnerText);
            Guid bikeGuid = Guid.Parse(bookingNode.ChildNodes[3]!.InnerText);
            DateTime startDate = DateTime.Parse(bookingNode.ChildNodes[4]!.InnerText);
            DateTime endDate = DateTime.Parse(bookingNode.ChildNodes[5]!.InnerText);
            double totalPrice = double.Parse(bookingNode.ChildNodes[6]!.InnerText);


            var customers = DeserializeCustomerList();
            var categories = DeserializeCategories();

            Customer customer = customers.Find(c => c.Guid == customerGuid)!;

            EBike_Category category = categories.Find(c => c.Guid == categoryGuid)!;

            EBike bike = category.Bikes.Find(b => b.Guid == bikeGuid)!;

            Booking booking = new Booking(guid, customer, category, bike, startDate, endDate, totalPrice);
            bookings.Add(booking);
        }
        return bookings;
    }


    //DIese Methode serialisiert die Buchungsliste in eine XML-Datei.
    public static void SerializeBookingList(List<Booking> bookings)
    {
        XmlDocument doc = new XmlDocument();
        XmlNode rootNode = doc.CreateElement("Bookings");
        doc.AppendChild(rootNode);
        foreach (Booking booking in bookings)
        {
            XmlNode bookingNode = doc.CreateElement("Booking");
            rootNode.AppendChild(bookingNode);

            XmlNode guidNode = doc.CreateElement("Guid");
            guidNode.InnerText = booking.Guid.ToString();
            bookingNode.AppendChild(guidNode);
            //the customer element
            XmlNode customerNode = doc.CreateElement("Customer");
            bookingNode.AppendChild(customerNode);

            XmlNode customerGuidNode = doc.CreateElement("Guid");
            customerGuidNode.InnerText = booking.BookingCustomer.Guid.ToString();
            customerNode.AppendChild(customerGuidNode);

            XmlNode customerNameNode = doc.CreateElement("FirstName");
            customerNameNode.InnerText = booking.BookingCustomer.FirstName;
            customerNode.AppendChild(customerNameNode);

            XmlNode customerLastNameNode = doc.CreateElement("LastName");
            customerLastNameNode.InnerText = booking.BookingCustomer.LastName;
            customerNode.AppendChild(customerLastNameNode);

            XmlNode customerAddressNode = doc.CreateElement("Address");
            customerAddressNode.InnerText = booking.BookingCustomer.Address;
            customerNode.AppendChild(customerAddressNode);
            //the bike category node
            XmlNode bikeCategoryNode = doc.CreateElement("EBikeCategory");
            bookingNode.AppendChild(bikeCategoryNode);

            XmlNode bikeCategoryGuidNode = doc.CreateElement("Guid");
            bikeCategoryGuidNode.InnerText = booking.BookingCategory.Guid.ToString();
            bikeCategoryNode.AppendChild(bikeCategoryGuidNode);

            XmlNode bikeCategoryNameNode = doc.CreateElement("Name");
            bikeCategoryNameNode.InnerText = booking.BookingCategory.Name;
            bikeCategoryNode.AppendChild(bikeCategoryNameNode);

            XmlNode bikeCategoryRentalPricePerDayNode = doc.CreateElement("RentalPricePerDay");
            bikeCategoryRentalPricePerDayNode.InnerText = booking.BookingCategory.RentalPricePerDay.ToString();
            bikeCategoryNode.AppendChild(bikeCategoryRentalPricePerDayNode);

            XmlNode bikeCategoryRentalPricePerWeekNode = doc.CreateElement("RentalPricePerWeek");
            bikeCategoryRentalPricePerWeekNode.InnerText = booking.BookingCategory.RentalPricePerWeek.ToString();
            bikeCategoryNode.AppendChild(bikeCategoryRentalPricePerWeekNode);

            XmlNode maxSpeedNode = doc.CreateElement("MaxSpeed");
            maxSpeedNode.InnerText = booking.BookingCategory.MaxSpeed.ToString();
            bikeCategoryNode.AppendChild(maxSpeedNode);

            XmlNode bikeNode = doc.CreateElement("EBike");
            bookingNode.AppendChild(bikeNode);

            XmlNode bikeGuidNode = doc.CreateElement("Guid");
            bikeGuidNode.InnerText = booking.BookingBike.Guid.ToString();
            bikeNode.AppendChild(bikeGuidNode);

            XmlNode bikeProducerNode = doc.CreateElement("Producer");
            bikeProducerNode.InnerText = booking.BookingBike.Producer;
            bikeNode.AppendChild(bikeProducerNode);

            XmlNode bikeModelNode = doc.CreateElement("Model");
            bikeModelNode.InnerText = booking.BookingBike.Model;
            bikeNode.AppendChild(bikeModelNode);

            XmlNode bikePriceNode = doc.CreateElement("Performance");
            bikePriceNode.InnerText = booking.BookingBike.Performance.ToString();
            bikeNode.AppendChild(bikePriceNode);

            XmlNode startDateNode = doc.CreateElement("StartDate");
            startDateNode.InnerText = booking.BookingStart.ToString();
            bookingNode.AppendChild(startDateNode);

            XmlNode endDateNode = doc.CreateElement("EndDate");
            endDateNode.InnerText = booking.BookingEnd.ToString();
            bookingNode.AppendChild(endDateNode);

            XmlNode totalPriceNode = doc.CreateElement("TotalPrice");
            totalPriceNode.InnerText = booking.TotalCost.ToString();
            bookingNode.AppendChild(totalPriceNode);
        }
        doc.Save(bookingsFile);
    }


    //create the deserializer method in the style of the serialize booking list method
    public static List<Booking> DeserializeBookingList()
    {
        List<Booking> bookings = new List<Booking>();
        XmlDocument doc = new XmlDocument();
        doc.Load(bookingsFile);
        XmlNode rootNode = doc.DocumentElement!;
        foreach (XmlNode bookingNode in rootNode.ChildNodes)
        {
            Guid guid = Guid.Parse(bookingNode.ChildNodes[0]!.InnerText);
            Guid customerGuid = Guid.Parse(bookingNode.ChildNodes[1]!.ChildNodes[0]!.InnerText);
            string customerFirstName = bookingNode.ChildNodes[1]!.ChildNodes[1]!.InnerText;
            string customerLastName = bookingNode.ChildNodes[1]!.ChildNodes[2]!.InnerText;
            string customerAddress = bookingNode.ChildNodes[1]!.ChildNodes[3]!.InnerText;
            Guid categoryGuid = Guid.Parse(bookingNode.ChildNodes[2]!.ChildNodes[0]!.InnerText);
            string categoryName = bookingNode.ChildNodes[2]!.ChildNodes[1]!.InnerText;
            double categoryRentalPricePerDay = double.Parse(bookingNode.ChildNodes[2]!.ChildNodes[2]!.InnerText);
            double categoryRentalPricePerWeek = double.Parse(bookingNode.ChildNodes[2]!.ChildNodes[3]!.InnerText);
            int categoryMaxSpeed = int.Parse(bookingNode.ChildNodes[2]!.ChildNodes[4]!.InnerText);
            Guid bikeGuid = Guid.Parse(bookingNode.ChildNodes[3]!.ChildNodes[0]!.InnerText);
            string bikeProducer = bookingNode.ChildNodes[3]!.ChildNodes[1]!.InnerText;
            string bikeModel = bookingNode.ChildNodes[3]!.ChildNodes[2]!.InnerText;
            int bikePerformance = int.Parse(bookingNode.ChildNodes[3]!.ChildNodes[3]!.InnerText);
            DateTime startDate = DateTime.Parse(bookingNode.ChildNodes[4]!.InnerText);
            DateTime endDate = DateTime.Parse(bookingNode.ChildNodes[5]!.InnerText, new CultureInfo("de-DE"));
            double totalPrice = double.Parse(bookingNode.ChildNodes[6]!.InnerText);

            Customer customer = new Customer(customerGuid, customerFirstName, customerLastName, customerAddress);
            EBike_Category category = new EBike_Category(categoryGuid, categoryName, categoryRentalPricePerDay, categoryRentalPricePerWeek, categoryMaxSpeed);
            EBike bike = new EBike(bikeProducer, bikeModel, bikePerformance);
            Booking booking = new Booking(guid, customer, category, bike, startDate, endDate, totalPrice);
            bookings.Add(booking);

        }
        return bookings;

    }







    //create the methods that serializes the list of bookings




    public static void SerializeBookings(List<Booking> bookings)
    {
        XmlDocument doc = new XmlDocument();
        XmlNode rootNode = doc.CreateElement("Bookings");
        doc.AppendChild(rootNode);
        foreach (Booking booking in bookings)
        {
            XmlNode bookingNode = doc.CreateElement("Booking");
            rootNode.AppendChild(bookingNode);

            XmlNode guidNode = doc.CreateElement("Guid");
            guidNode.InnerText = booking.Guid.ToString();
            bookingNode.AppendChild(guidNode);

            XmlNode customerNode = doc.CreateElement("CustomerID");
            customerNode.InnerText = booking.BookingCustomer.Guid.ToString();
            bookingNode.AppendChild(customerNode);

            XmlNode bikeCategoryNode = doc.CreateElement("EBikeCategoryID");
            bikeCategoryNode.InnerText = booking.BookingCategory.Guid.ToString();
            bookingNode.AppendChild(bikeCategoryNode);

            XmlNode bikeGuidNode = doc.CreateElement("EBikeID");
            bikeGuidNode.InnerText = booking.BookingBike.Guid.ToString();
            bookingNode.AppendChild(bikeGuidNode);

            XmlNode startDateNode = doc.CreateElement("StartDate");
            startDateNode.InnerText = booking.BookingStart.ToString();
            bookingNode.AppendChild(startDateNode);

            XmlNode endDateNode = doc.CreateElement("EndDate");
            endDateNode.InnerText = booking.BookingEnd.ToString();
            bookingNode.AppendChild(endDateNode);

            XmlNode totalPriceNode = doc.CreateElement("TotalPrice");
            totalPriceNode.InnerText = booking.TotalCost.ToString();
            bookingNode.AppendChild(totalPriceNode);
        }
        doc.Save("bookings.xml");
    }




    //Für die Entfernung des Warning-Hinweises habe ich folgenden Link genutzt: 
    //https://stackoverflow.com/questions/11228855/how-to-remove-warning-cs0618-xmldocument-save-is-obsolete

}
