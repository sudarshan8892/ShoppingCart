using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    [Table("OrderStatus")]
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int? StatusId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string StatusName { get; set; } = null!;

        [InverseProperty(nameof(Order.OrderStatus))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
