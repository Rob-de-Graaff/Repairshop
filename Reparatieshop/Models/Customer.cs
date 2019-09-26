using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reparatieshop.Models
{
    [Table("Customers")]
    public class Customer
    {
        public Customer()
        {
                
        }
        public Customer(RegisterCustomerViewModel cvm)
        {
            this.CustomerId = cvm.CustomerId;
            this.FirstName = cvm.FirstName;
            this.LastName = cvm.LastName;
            this.DoB = cvm.DoB;
            this.City = cvm.City;
            this.Street = cvm.Street;
            this.Zipcode = cvm.Zipcode;
            this.HouseNumber = cvm.HouseNumber;
            this.Assignments = cvm.Assignments;
        }

        [Key]
        public string CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FirstLastName
        {
            get { return $"{FirstName} {LastName}";
            }
        }
        [Required]
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

    [NotMapped]
    public class RegisterCustomerViewModel : Customer
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}