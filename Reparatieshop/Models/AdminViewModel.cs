using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reparatieshop.Models
{
    public class AdminViewModel
    {
        public AdminViewModel() { }
        public AdminViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}