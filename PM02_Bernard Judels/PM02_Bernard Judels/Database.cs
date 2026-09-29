using System;
using Microsoft.Data.SqlClient;

namespace SAA
{
    public class Database
    {
        private readonly string connectionString =
            "Server=BERNARD;Database=SAA;" +
            "Integrated Security=True;" +
            "Encrypt=True;TrustServerCertificate=True;";

        private void DisplayQuery(string query)
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command =
                        new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No records found.");
                            return;
                        }

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string value = reader.IsDBNull(i)
                                    ? "(empty)"
                                    : Convert.ToString(reader.GetValue(i))
                                        ?? string.Empty;

                                Console.WriteLine(
                                    reader.GetName(i) + ": " + value);
                            }

                            Console.WriteLine("-------------------------");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
        }

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine(
                        "Connected successfully!");
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return false;
            }
        }

        public void DisplayAllAirports()
        {
            DisplayQuery(@"
                SELECT DestinationID, Country, Airport, City, CityCode
                FROM dbo.Destination
                ORDER BY Country, City;");
        }

        public void DisplayAllFlights()
        {
            DisplayQuery(@"
                SELECT
                    f.FlightID,
                    f.FlightNumber,
                    departure.Airport AS DepartureAirport,
                    arrival.Airport AS ArrivalAirport,
                    f.DepartureDateTime,
                    f.ArrivalDateTime,
                    f.CheckInClosingDateTime,
                    f.FlightStatus
                FROM dbo.Flight AS f
                INNER JOIN dbo.Destination AS departure
                    ON departure.DestinationID =
                       f.DepartureDestinationID
                INNER JOIN dbo.Destination AS arrival
                    ON arrival.DestinationID =
                       f.ArrivalDestinationID
                ORDER BY f.DepartureDateTime;");
        }

        public void DisplayAllPassengers()
        {
            DisplayQuery(@"
                SELECT *
                FROM dbo.Passenger
                ORDER BY PassengerID;");
        }

        public void DisplayAllPassengerDocuments()
        {
            DisplayQuery(@"
                SELECT *
                FROM dbo.PassengerDocument
                ORDER BY PassengerDocumentID;");
        }

        public void DisplayAllBookings()
        {
            DisplayQuery(@"
                SELECT
                    b.BookingID,
                    b.BookingReference,
                    p.FirstName,
                    p.Surname,
                    f.FlightNumber,
                    b.BookingDateTime,
                    b.BookingStatus
                FROM dbo.Booking AS b
                INNER JOIN dbo.Passenger AS p
                    ON p.PassengerID = b.PassengerID
                INNER JOIN dbo.Flight AS f
                    ON f.FlightID = b.FlightID
                ORDER BY b.BookingID;");
        }

        public void DisplayAllPayments()
        {
            DisplayQuery(@"
                SELECT
                    p.PaymentID,
                    b.BookingReference,
                    p.Amount,
                    p.IsPaid,
                    p.PaymentDateTime
                FROM dbo.Payment AS p
                INNER JOIN dbo.Booking AS b
                    ON b.BookingID = p.BookingID
                ORDER BY p.PaymentID;");
        }

        public void DisplayJohannesburgToCapeTownFlights()
        {
            DisplayQuery(@"
        SELECT
            f.FlightID,
            f.FlightNumber,
            departure.Airport AS DepartureAirport,
            arrival.Airport AS ArrivalAirport,
            f.DepartureDateTime,
            f.ArrivalDateTime,
            f.CheckInClosingDateTime,
            f.FlightStatus
        FROM dbo.Flight AS f
        INNER JOIN dbo.Destination AS departure
            ON departure.DestinationID = f.DepartureDestinationID
        INNER JOIN dbo.Destination AS arrival
            ON arrival.DestinationID = f.ArrivalDestinationID
        WHERE departure.CityCode = 'JNB'
          AND arrival.CityCode = 'CPT'
        ORDER BY f.DepartureDateTime;");
        }

        public void UpdatePassengerEmail(int passengerID, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Please give an email address.");
                return;
            }

            email = email.Trim();

            if (email.Length > 254)
            {
                Console.WriteLine("The email address is too long.");
                return;
            }

            ExecuteUpdate(
                @"UPDATE dbo.Passenger
          SET Email = @Email
          WHERE PassengerID = @PassengerID;",
                new SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 254)
                {
                    Value = email
                },
                new SqlParameter("@PassengerID", System.Data.SqlDbType.Int)
                {
                    Value = passengerID
                });
        }

        public void UpdateBookingStatus(int bookingID, string status)
        {
            string validStatus;

            switch ((status ?? "").Trim().ToLowerInvariant())
            {
                case "pending":
                    validStatus = "Pending";
                    break;
                case "confirmed":
                    validStatus = "Confirmed";
                    break;
                case "cancelled":
                    validStatus = "Cancelled";
                    break;
                case "completed":
                    validStatus = "Completed";
                    break;
                default:
                    Console.WriteLine(
                        "Use Pending, Confirmed, Cancelled, or Completed.");
                    return;
            }

            ExecuteUpdate(
                @"UPDATE dbo.Booking
          SET BookingStatus = @Status
          WHERE BookingID = @BookingID;",
                new SqlParameter("@Status", System.Data.SqlDbType.VarChar, 20)
                {
                    Value = validStatus
                },
                new SqlParameter("@BookingID", System.Data.SqlDbType.Int)
                {
                    Value = bookingID
                });
        }

        
        public void UpdatePaymentStatus(int bookingID, bool isPaid)
        {
            ExecuteUpdate(
                @"UPDATE dbo.Payment
          SET IsPaid = @IsPaid,
              PaymentDateTime =
                  CASE
                      WHEN @IsPaid = 0 THEN NULL
                      WHEN IsPaid = 1 THEN PaymentDateTime
                      ELSE SYSDATETIMEOFFSET()
                  END
          WHERE BookingID = @BookingID;",
                new SqlParameter("@IsPaid", System.Data.SqlDbType.Bit)
                {
                    Value = isPaid
                },
                new SqlParameter("@BookingID", System.Data.SqlDbType.Int)
                {
                    Value = bookingID
                });
        }

       
        private void ExecuteUpdate(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                using (SqlCommand command =
                    new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine(rowsAffected > 0
                        ? "Record updated successfully."
                        : "No matching record found.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Update failed: " + ex.Message);
            }
        }

        public void DisplayOverallBookingSummary()
        {
            DisplayQuery(@"
        SELECT
            COUNT(b.BookingID) AS TotalBookings,
            COUNT(DISTINCT b.PassengerID) AS DistinctPassengers,
            COUNT(DISTINCT b.FlightID) AS FlightsWithBookings,
            COALESCE(SUM(
                CASE WHEN b.BookingStatus = 'Pending'
                     THEN 1 ELSE 0 END
            ), 0) AS PendingBookings,
            COALESCE(SUM(
                CASE WHEN b.BookingStatus = 'Confirmed'
                     THEN 1 ELSE 0 END
            ), 0) AS ConfirmedBookings,
            COALESCE(SUM(
                CASE WHEN b.BookingStatus = 'Completed'
                     THEN 1 ELSE 0 END
            ), 0) AS CompletedBookings,
            COALESCE(SUM(
                CASE WHEN b.BookingStatus = 'Cancelled'
                     THEN 1 ELSE 0 END
            ), 0) AS CancelledBookings,
            COALESCE(SUM(
                CASE
                    WHEN b.BookingStatus IN ('Confirmed', 'Completed')
                    THEN 1 ELSE 0
                END
            ), 0) AS TicketsIssuedProxy,
            COALESCE(SUM(
                CASE WHEN pay.IsPaid = 1
                     THEN 1 ELSE 0 END
            ), 0) AS PaidBookings,
            COALESCE(SUM(
                CASE WHEN pay.PaymentID IS NULL
                     THEN 1 ELSE 0 END
            ), 0) AS BookingsWithoutPaymentRecord,
            COALESCE(SUM(
                CASE WHEN pay.IsPaid = 1
                     THEN pay.Amount ELSE 0 END
            ), 0) AS TotalRevenueReceivedRands,
            COALESCE(SUM(
                CASE
                    WHEN pay.IsPaid = 0
                         AND b.BookingStatus <> 'Cancelled'
                    THEN pay.Amount ELSE 0
                END
            ), 0) AS RecordedUnpaidAmountRands
        FROM dbo.Booking AS b
        LEFT JOIN dbo.Payment AS pay
            ON pay.BookingID = b.BookingID;");
        }

        public void DisplayRevenuePerFlight()
        {
            DisplayQuery(@"
        SELECT
            f.FlightID,
            f.FlightNumber,
            f.DepartureDateTime,
            departure.City AS DepartureCity,
            arrival.City AS ArrivalCity,
            COUNT(b.BookingID) AS TotalBookings,
            SUM(
                CASE
                    WHEN b.BookingStatus IN ('Confirmed', 'Completed')
                    THEN 1 ELSE 0
                END
            ) AS TicketsIssuedProxy,
            SUM(
                CASE
                    WHEN pay.IsPaid = 1 THEN pay.Amount
                    ELSE 0
                END
            ) AS RevenueReceivedRands
        FROM dbo.Flight AS f
        INNER JOIN dbo.Destination AS departure
            ON departure.DestinationID = f.DepartureDestinationID
        INNER JOIN dbo.Destination AS arrival
            ON arrival.DestinationID = f.ArrivalDestinationID
        LEFT JOIN dbo.Booking AS b
            ON b.FlightID = f.FlightID
        LEFT JOIN dbo.Payment AS pay
            ON pay.BookingID = b.BookingID
        GROUP BY
            f.FlightID,
            f.FlightNumber,
            f.DepartureDateTime,
            departure.City,
            arrival.City
        ORDER BY
            RevenueReceivedRands DESC,
            f.DepartureDateTime;");
        }

        public void DisplayFlightManifest()
        {
            DisplayQuery(@"
        SELECT
            f.FlightNumber,
            f.DepartureDateTime,
            departure.City AS DepartureCity,
            arrival.City AS ArrivalCity,
            b.BookingReference,
            p.FirstName,
            p.Surname,
            b.BookingStatus,
            CASE
                WHEN pay.PaymentID IS NULL THEN 'No payment record'
                WHEN pay.IsPaid = 1 THEN 'Paid'
                ELSE 'Unpaid'
            END AS PaymentStatus
        FROM dbo.Booking AS b
        INNER JOIN dbo.Passenger AS p
            ON p.PassengerID = b.PassengerID
        INNER JOIN dbo.Flight AS f
            ON f.FlightID = b.FlightID
        INNER JOIN dbo.Destination AS departure
            ON departure.DestinationID = f.DepartureDestinationID
        INNER JOIN dbo.Destination AS arrival
            ON arrival.DestinationID = f.ArrivalDestinationID
        LEFT JOIN dbo.Payment AS pay
            ON pay.BookingID = b.BookingID
        WHERE b.BookingStatus <> 'Cancelled'
        ORDER BY
            f.DepartureDateTime,
            f.FlightNumber,
            p.Surname,
            p.FirstName;");
        }
    }
}