#nullable enable
using System;

namespace SAA.Models
{
    public class Passenger
    {
        private int _passengerID;
        private string _firstName = string.Empty;
        private string _surname = string.Empty;
        private DateTime _dateOfBirth;
        private string? _nationality;
        private string? _email;
        private string? _phoneNumber;

        public int GetPassengerID()
        {
            return _passengerID;
        }

        public void SetPassengerID(int value)
        {
            _passengerID = value;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public void SetFirstName(string value)
        {
            _firstName = value;
        }

        public string GetSurname()
        {
            return _surname;
        }

        public void SetSurname(string value)
        {
            _surname = value;
        }

        public DateTime GetDateOfBirth()
        {
            return _dateOfBirth;
        }

        public void SetDateOfBirth(DateTime value)
        {
            _dateOfBirth = value;
        }

        public string? GetNationality()
        {
            return _nationality;
        }

        public void SetNationality(string? value)
        {
            _nationality = value;
        }

        public string? GetEmail()
        {
            return _email;
        }

        public void SetEmail(string? value)
        {
            _email = value;
        }

        public string? GetPhoneNumber()
        {
            return _phoneNumber;
        }

        public void SetPhoneNumber(string? value)
        {
            _phoneNumber = value;
        }
    }
}

