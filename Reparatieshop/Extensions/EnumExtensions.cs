using Reparatieshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reparatieshop.Extensions
{
    /// <summary>
    /// This is example code, it is not implemented
    /// </summary>
    public static class EnumExtensions
    {
        public static int HoeveelheidKlaar(this IEnumerable<Assignment> assignments)
            => assignments.Where(x => x.Status == Status.Done).Count();

    }
}