﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BackEndFlutter.Models.Data
{
    public partial class Tables
    {
        public Tables()
        {
            Orders = new HashSet<Orders>();
        }

        public int TableId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}