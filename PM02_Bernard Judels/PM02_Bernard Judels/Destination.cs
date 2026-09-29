#nullable enable
using System;

namespace SAA.Models
{
    public class Destination
    {
        private int _destinationID;
        private string _country = string.Empty;
        private string _airport = string.Empty;
        private string _city = string.Empty;
        private string _cityCode = string.Empty;

        public int GetDestinationID()
        {
            return _destinationID;
        }

        public void SetDestinationID(int value)
        {
            _destinationID = value;
        }

        public string GetCountry()
        {
            return _country;
        }

        public void SetCountry(string value)
        {
            _country = value;
        }

        public string GetAirport()
        {
            return _airport;
        }

        public void SetAirport(string value)
        {
            _airport = value;
        }

        public string GetCity()
        {
            return _city;
        }

        public void SetCity(string value)
        {
            _city = value;
        }

        public string GetCityCode()
        {
            return _cityCode;
        }

        public void SetCityCode(string value)
        {
            _cityCode = value;
        }
    }
}

