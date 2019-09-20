using Reparatieshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reparatieshop.Extensions
{
    public static class ColorExtension
    {
        public static string CheckDate(Assignment assignment)
        {
            string color = "black";

            if (DateTime.Now >= assignment.Start.Date && assignment.Status != Status.Done)
            {
                color = "red";
            }
            return color;
        }

        public static string CheckStatus(Assignment assignment)
        {
            string color = "black";

            if (assignment.Status == Status.Done)
            {
                color = "green";
            }
            return color;
        }
    }
}