using ElectroStore.Domain.Entities;
using ElectroStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectroStore.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}