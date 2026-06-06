using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shopper.Models
{
    public class DeliveryDetails
    {
        public int DeliveryDetailsId { get; set; }
        public string Region { get; set; }     // Woj
        public string City { get; set; }       // miasto
        public string Street { get; set; }     // ulica
        public string TownCode { get; set; }   // kod pocztowy
        public string HomeNumber { get; set; } // nr domu

        // W C# używamy DateTime zamiast java.util.Date
        public DateTime DeliveredDate { get; set; }

        public double DeliveryCost { get; set; }

        // --- RELACJE JEDEN-DO-JEDNEGO (OneToOne) ---

        // Klucz obcy dla użytkownika
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; }

        // Klucz obcy dla zamówienia
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Orders Order { get; set; }
    }
}
