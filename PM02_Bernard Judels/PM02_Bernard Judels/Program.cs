using System;

namespace SAA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            Console.WriteLine("SAA DATABASE");
            Console.WriteLine("Connecting to the database...");

            if (!database.TestConnection())
            {
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
                return;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Display all airports");
                Console.WriteLine("2. Display all flights");
                Console.WriteLine("3. Display all passengers");
                Console.WriteLine("4. Display all passenger documents");
                Console.WriteLine("5. Display all bookings");
                Console.WriteLine("6. Display all payments");
                Console.WriteLine("7. Flights from Johannesburg to Cape Town");
                Console.WriteLine("8. Update passenger email");
                Console.WriteLine("9. Update booking status");
                Console.WriteLine("10. Update payment status");
                Console.WriteLine("11. Passenger bookings and flight manifest");
                Console.WriteLine("12. Revenue and bookings per flight");
                Console.WriteLine("13. Overall booking summary");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "0";
                Console.WriteLine();

                switch (choice.Trim())
                {
                    case "1":
                        database.DisplayAllAirports();
                        break;
                    case "2":
                        database.DisplayAllFlights();
                        break;
                    case "3":
                        database.DisplayAllPassengers();
                        break;
                    case "4":
                        database.DisplayAllPassengerDocuments();
                        break;
                    case "5":
                        database.DisplayAllBookings();
                        break;
                    case "6":
                        database.DisplayAllPayments();
                        break;
                    case "7":
                        database.DisplayJohannesburgToCapeTownFlights();
                        break;
                    case "8":
                        UpdatePassengerEmail(database);
                        break;

                    case "9":
                        UpdateBookingStatus(database);
                        break;

                    case "10":
                        UpdatePaymentStatus(database);
                        break;
                    case "11":
                        database.DisplayFlightManifest();
                        break;

                    case "12":
                        database.DisplayRevenuePerFlight();
                        break;

                    case "13":
                        database.DisplayOverallBookingSummary();
                        break;
                    case "0":
                        Console.WriteLine("bye!");
                        return;
                    default:
                        Console.WriteLine("Please choose a number from 0 to 6.");
                        continue;
                }

                Console.WriteLine("Press Enter to return to the menu.");
                Console.ReadLine();
            }
        }

        private static void UpdatePassengerEmail(Database database)
        {
            Console.Write("Enter Passenger ID: ");

            if (!int.TryParse(Console.ReadLine(), out int passengerID)
                || passengerID <= 0)
            {
                Console.WriteLine("Please enter a valid positive Passenger ID.");
                return;
            }

            Console.Write("Enter the new email address: ");
            string email = Console.ReadLine() ?? string.Empty;

            database.UpdatePassengerEmail(passengerID, email);
        }

        private static void UpdateBookingStatus(Database database)
        {
            Console.Write("Enter Booking ID: ");

            if (!int.TryParse(Console.ReadLine(), out int bookingID)
                || bookingID <= 0)
            {
                Console.WriteLine("Please enter a valid positive Booking ID.");
                return;
            }

            Console.WriteLine("Available statuses:");
            Console.WriteLine("1. Pending");
            Console.WriteLine("2. Confirmed");
            Console.WriteLine("3. Cancelled");
            Console.WriteLine("4. Completed");
            Console.Write("Choose a status: ");


            string status;

            switch (Console.ReadLine())
            {
                case "1":
                    status = "Pending";
                    break;
                case "2":
                    status = "Confirmed";
                    break;
                case "3":
                    status = "Cancelled";
                    break;
                case "4":
                    status = "Completed";
                    break;
                default:
                    Console.WriteLine("Invalid status selection.");
                    return;
            }

            database.UpdateBookingStatus(bookingID, status);
        }

        private static void UpdatePaymentStatus(Database database)
        {
            Console.Write("Enter Booking ID for the payment: ");

            if (!int.TryParse(Console.ReadLine(), out int bookingID)
                || bookingID <= 0)
            {
                Console.WriteLine("Please enter a valid positive Booking ID.");
                return;
            }

            Console.WriteLine("1. Paid");
            Console.WriteLine("2. Unpaid");
            Console.Write("Choose a payment status: ");

            switch (Console.ReadLine())
            {
                case "1":
                    database.UpdatePaymentStatus(bookingID, true);
                    break;
                case "2":
                    database.UpdatePaymentStatus(bookingID, false);
                    break;
                default:
                    Console.WriteLine("Invalid payment status selection.");
                    break;
            }
        }
    }
}
