using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Reparatieshop.Models
{
    [Table("Repairers")]
    public class Repairer
    {
        public Repairer()
        {

        }

        public Repairer(RegisterRepairerViewModel rvm)
        {
            this.RepairerId = rvm.RepairerId;
            this.FirstName = rvm.FirstName;
            this.LastName = rvm.LastName;
            this.DoB = rvm.DoB;
            this.Wage = rvm.Wage;
        }

        [Key]
        public string RepairerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FirstLastName { get
            {
                return $"{FirstName} {LastName}";
            } }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DoB { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Wage { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }

    [NotMapped]
    public class RegisterRepairerViewModel : Repairer
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