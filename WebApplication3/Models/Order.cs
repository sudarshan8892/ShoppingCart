using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            Orderdetails = new HashSet<Orderdetail>();
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? OrderStatusId { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey(nameof(OrderStatusId))]
        [InverseProperty("Orders")]
        public virtual OrderStatus? OrderStatus { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Orders")]
        public virtual User? User { get; set; }
        [InverseProperty(nameof(Orderdetail.Order))]
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
    }
}
