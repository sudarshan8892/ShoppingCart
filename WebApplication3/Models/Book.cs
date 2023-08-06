using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    [Table("Book")]
    public partial class Book
    {
        public Book()
        {
            CartDetails = new HashSet<CartDetail>();
            Orderdetails = new HashSet<Orderdetail>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? BookName { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        public byte[]? BookImage { get; set; }
        public int? GenerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        
        public string? AuthorName { get; set; }
        [NotMapped]
        public string GenerName { get; set; }


        [ForeignKey(nameof(GenerId))]
        [InverseProperty(nameof(Genre.Books))]
        public virtual Genre? Gener { get; set; }
        [InverseProperty(nameof(CartDetail.Booking))]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        [InverseProperty(nameof(Orderdetail.Book))]
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
    }
}
