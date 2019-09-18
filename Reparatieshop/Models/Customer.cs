using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Reparatieshop.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DoB { get; set; }
        [Required]
        public string City { get; set; }
        public string Street { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public int HouseNumber { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}