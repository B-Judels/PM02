#nullable enable
using System;

namespace SAA.Models
{
    public class PassengerDocument
    {
        private int _passengerDocumentID;
        private int _passengerID;
        private string _documentType = string.Empty;
        private string _documentNumber = string.Empty;
        private string _issuingCountry = string.Empty;
        private DateTime? _expiryDate;

        public int GetPassengerDocumentID()
        {
            return _passengerDocumentID;
        }

        public void SetPassengerDocumentID(int value)
        {
            _passengerDocumentID = value;
        }

        public int GetPassengerID()
        {
            return _passengerID;
        }

        public void SetPassengerID(int value)
        {
            _passengerID = value;
        }

        public string GetDocumentType()
        {
            return _documentType;
        }

        public void SetDocumentType(string value)
        {
            _documentType = value;
        }

        public string GetDocumentNumber()
        {
            return _documentNumber;
        }

        public void SetDocumentNumber(string value)
        {
            _documentNumber = value;
        }

        public string GetIssuingCountry()
        {
            return _issuingCountry;
        }

        public void SetIssuingCountry(string value)
        {
            _issuingCountry = value;
        }

        public DateTime? GetExpiryDate()
        {
            return _expiryDate;
        }

        public void SetExpiryDate(DateTime? value)
        {
            _expiryDate = value;
        }
    }
}

