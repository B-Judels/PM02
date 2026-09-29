#nullable enable
using System;

namespace SAA.Models
{
    public class Flight
    {
        private int _flightID;
        private string _flightNumber = string.Empty;
        private int _departureDestinationID;
        private int _arrivalDestinationID;
        private DateTimeOffset _departureDateTime;
        private DateTimeOffset _arrivalDateTime;
        private DateTimeOffset _checkInClosingDateTime;
        private string _flightStatus = "Scheduled";

        public int GetFlightID()
        {
            return _flightID;
        }

        public void SetFlightID(int value)
        {
            _flightID = value;
        }

        public string GetFlightNumber()
        {
            return _flightNumber;
        }

        public void SetFlightNumber(string value)
        {
            _flightNumber = value;
        }

        public int GetDepartureDestinationID()
        {
            return _departureDestinationID;
        }

        public void SetDepartureDestinationID(int value)
        {
            _departureDestinationID = value;
        }

        public int GetArrivalDestinationID()
        {
            return _arrivalDestinationID;
        }

        public void SetArrivalDestinationID(int value)
        {
            _arrivalDestinationID = value;
        }

        public DateTimeOffset GetDepartureDateTime()
        {
            return _departureDateTime;
        }

        public void SetDepartureDateTime(DateTimeOffset value)
        {
            _departureDateTime = value;
        }

        public DateTimeOffset GetArrivalDateTime()
        {
            return _arrivalDateTime;
        }

        public void SetArrivalDateTime(DateTimeOffset value)
        {
            _arrivalDateTime = value;
        }

        public DateTimeOffset GetCheckInClosingDateTime()
        {
            return _checkInClosingDateTime;
        }

        public void SetCheckInClosingDateTime(DateTimeOffset value)
        {
            _checkInClosingDateTime = value;
        }

        public string GetFlightStatus()
        {
            return _flightStatus;
        }

        public void SetFlightStatus(string value)
        {
            _flightStatus = value;
        }
    }
}

