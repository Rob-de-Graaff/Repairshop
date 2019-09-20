using Reparatieshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reparatieshop.Extensions
{
    public static class IntExtention
    {
        public static int CalculateTotal(Status status, IQueryable<Assignment> assignments)
        {
            int total = assignments.Where(x => x.Status == status).Count();
            return total;
        }
    }
}