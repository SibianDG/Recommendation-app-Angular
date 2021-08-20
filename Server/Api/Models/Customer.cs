using System.Collections.Generic;

namespace Api.Models
{
    public class Customer
    {
        #region Properties
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        #endregion

        #region Constructors
        public Customer()
        {
        }
        #endregion

    }
}