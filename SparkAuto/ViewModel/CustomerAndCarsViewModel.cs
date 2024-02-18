using SparkAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.ViewModel
{
    public class CustomerAndCarsViewModel
    {
        public ApplicationUser UserObject { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
