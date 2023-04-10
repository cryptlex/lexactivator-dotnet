using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class OrganizationAddress
    {
        public string AddressLine1;

        public string AddressLine2;

        public string City;

        public string State;

        public string Country;

        public string PostalCode;

        public OrganizationAddress(string addressLine1, string addressLine2, string city, string state, string country, string postalCode)
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.PostalCode = postalCode;
        }
    }
}
