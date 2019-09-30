using Reparatieshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reparatieshop.Extensions
{
    public static class StringExtension
    {
        public static string CalculateCosts(Assignment selectedAssignment)
        {
            double totalCosts = 0.00d;

            foreach (Product product in selectedAssignment.Products)
            {
                totalCosts += product.Price;
            }

            totalCosts += selectedAssignment.Repairer.Wage * selectedAssignment.HoursWorked;

            return string.Format("{0:C2}", totalCosts);
        }
    }
}