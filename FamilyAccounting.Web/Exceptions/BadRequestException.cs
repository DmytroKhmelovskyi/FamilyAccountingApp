using System;
using System.Globalization;

namespace FamilyAccounting.Web.Services
{
    internal class BadRequestException:Exception
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}