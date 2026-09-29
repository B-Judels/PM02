#nullable enable
using System;

namespace SAA.Models
{
    public class Booking
    {
        private int _bookingID;
        private int _passengerID;
        private int _flightID;
        private string _bookingReference = string.Empty;
        private DateTimeOffset _bookingDateTime = DateTimeOffset.UtcNow;
        private string _bookingStatus = "Pending";
        private string _contactEmail = string.Empty;
        private string? _contactPhoneNumber;

        public int GetBookingID()
        {
            return _bookingID;
        }

        public void SetBookingID(int value)
        {
            _bookingID = value;
        }

        public int GetPassengerID()
        {
            return _passengerID;
        }

        public void SetPassengerID(int value)
        {
            _passengerID = value;
        }

        public int GetFlightID()
        {
            return _flightID;
        }

        public void SetFlightID(int value)
        {
            _flightID = value;
        }

        public string GetBookingReference()
        {
            return _bookingReference;
        }

        public void SetBookingReference(string value)
        {
            _bookingReference = value;
        }

        public DateTimeOffset GetBookingDateTime()
        {
            return _bookingDateTime;
        }

        public void SetBookingDateTime(DateTimeOffset value)
        {
            _bookingDateTime = value;
        }

        public string GetBookingStatus()
        {
            return _bookingStatus;
        }

        public void SetBookingStatus(string value)
        {
            _bookingStatus = value;
        }

        public string GetContactEmail()
        {
            return _contactEmail;
        }

        public void SetContactEmail(string value)
        {
            _contactEmail = value;
        }

        public string? GetContactPhoneNumber()
        {
            return _contactPhoneNumber;
        }

        public void SetContactPhoneNumber(string? value)
        {
            _contactPhoneNumber = value;
        }
    }
}

