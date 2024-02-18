﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Models
{
    public class Service
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public double Miles { get; set; }
        public string Comments { get; set; }
        [DisplayFormat(DataFormatString ="{0:MMM dd yyyy}")]
        public DateTime DateAdded { get; set; }

        public int CarId { get; set; }
        [ForeignKey("CarId")]

        public virtual Car Car { get; set; }
    }
}

