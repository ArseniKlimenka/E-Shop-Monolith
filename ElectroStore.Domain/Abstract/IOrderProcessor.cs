using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectroStore.Domain.Entities;

namespace ElectroStore.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
