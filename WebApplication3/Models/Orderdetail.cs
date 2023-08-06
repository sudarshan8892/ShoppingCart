using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public partial class Orderdetail
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal UnitPrice { get; set; }
        public int Qunitity { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        [InverseProperty("Orderdetails")]
        public virtual Book Book { get; set; } = null!;
        [ForeignKey(nameof(OrderId))]
        [InverseProperty("Orderdetails")]
        public virtual Order Order { get; set; } = null!;
    }
}
