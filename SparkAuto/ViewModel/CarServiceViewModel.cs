using SparkAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.ViewModel
{
    public class CarServiceViewModel
    {
        public Car Car { get; set; }
        public Service Service { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
        public List<ServiceType> ServiceTypes { get; set; }
        public List<ServiceShoppingCart> ServiceShoppingCarts { get; set; }
    }
}
