using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Reparatieshop.Models
{
    public enum Status
    {
        Pending,
        in_progress,
        Waiting_for_parts, 
        Done
    }

    [Table("Assignments")]
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        [Required]
        public Status Status { get; set; }
        public string Description { get; set; }
        [Required]
        public int HoursWorked { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Repairer Repairer { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}