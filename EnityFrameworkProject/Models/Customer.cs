using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnityFrameworkProject.Models
{
    /// <summary>
    /// Klasa Customer reprezentuje klienta sklepu
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Identyfikator jednoznacznie określający klienta
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Imię klienta sklepu
        /// </summary>
        public string FirstName { get; set; } = null!;
        /// <summary>
        /// Nazwisko klienta sklepu
        /// </summary>
        public string LastName { get; set; } = null!;
        /// <summary>
        /// e-mail klienta sklepu
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Numer telefonu klienta sklepu
        /// </summary>
        public string Phone { get; set; } = null!;
        /// <summary>
        /// Lista zamówień dla klienta sklepu
        /// </summary>
        public ICollection<Order> Orders { get; set; } = null!;

    }
}
