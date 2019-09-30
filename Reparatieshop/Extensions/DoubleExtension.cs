using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reparatieshop.Extensions
{
    public static class DoubleExtension
    {
        public static double ConvertInput(string input)
        {
            double resultDouble;
            input = input.Replace('.', ',');

            if (!double.TryParse(input, out resultDouble))
            {
                resultDouble = 0.0;
            }

            return resultDouble = Math.Round(resultDouble, 2); ;
        }
    }
}