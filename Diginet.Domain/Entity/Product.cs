﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diginet.Domain.Entity
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string ExternalSystem_ID { get; set; }
        public string Product_Name { get; set; }
        public string Product_Description { get; set; }
        public decimal Price { get; set; }
        public decimal SVP { get; set; }
        public int Currency_ID { get; set; }
        public int Is_Active { get; set; }
        public DateTime Creation_Date { get; set; }
        public int Updated_By { get; set; }
        public DateTime Updated_Date { get; set; }
    }
}
