using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public partial class CartDetail
    {
        [Key]
        public int Id { get; set; }
        public int Qunitity { get; set; }
        [Column("Booking_Id")]
        public int BookingId { get; set; }
        [Column("Shopping_Id")]
        public int? ShoppingId { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal UnitPrice { get; set; }

        [ForeignKey(nameof(BookingId))]
        [InverseProperty(nameof(Book.CartDetails))]
        public virtual Book? Booking { get; set; }
        [ForeignKey(nameof(ShoppingId))]
        [InverseProperty(nameof(ShoppingCart.CartDetails))]
        public virtual ShoppingCart? Shopping { get; set; }
    }
}
