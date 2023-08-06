using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    [Table("ShoppingCart")]
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("ShoppingCarts")]
        public virtual User? User { get; set; }
        [InverseProperty(nameof(CartDetail.Shopping))]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
