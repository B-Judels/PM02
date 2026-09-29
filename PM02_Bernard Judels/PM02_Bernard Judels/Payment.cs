#nullable enable
using System;

namespace SAA.Models
{
    public class Payment
    {
        private int _paymentID;
        private int _bookingID;
        private decimal _amount;
        private bool _isPaid;
        private DateTimeOffset? _paymentDateTime;

        public int GetPaymentID()
        {
            return _paymentID;
        }

        public void SetPaymentID(int value)
        {
            _paymentID = value;
        }

        public int GetBookingID()
        {
            return _bookingID;
        }

        public void SetBookingID(int value)
        {
            _bookingID = value;
        }

        public decimal GetAmount()
        {
            return _amount;
        }

        public void SetAmount(decimal value)
        {
            _amount = value;
        }

        public bool GetIsPaid()
        {
            return _isPaid;
        }

        public void SetIsPaid(bool value)
        {
            _isPaid = value;
        }

        public DateTimeOffset? GetPaymentDateTime()
        {
            return _paymentDateTime;
        }

        public void SetPaymentDateTime(DateTimeOffset? value)
        {
            _paymentDateTime = value;
        }
    }
}

