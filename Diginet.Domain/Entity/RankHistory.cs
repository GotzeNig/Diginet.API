﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diginet.Domain.Entity
{
    public class RankHistory
    {
        [Key]
        public int ID { get; set; }
        public int Rank_ID { get; set; }
        public string Rank_Name { get; set; }
        public string Rank_Description { get; set; }
        public decimal RankMaximum_Points { get; set; }
        public decimal RankMinimum_Points { get; set; }
        public int Is_Active { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
