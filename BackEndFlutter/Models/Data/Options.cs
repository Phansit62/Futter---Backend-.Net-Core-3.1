﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BackEndFlutter.Models.Data
{
    public partial class Options
    {
        public Options()
        {
            Details = new HashSet<Details>();
            FoodOptions = new HashSet<FoodOptions>();
            OptionsDetail = new HashSet<OptionsDetail>();
        }

        public int OptionsId { get; set; }
        public string Titlename { get; set; }

        public virtual ICollection<Details> Details { get; set; }
        public virtual ICollection<FoodOptions> FoodOptions { get; set; }
        public virtual ICollection<OptionsDetail> OptionsDetail { get; set; }
    }
}