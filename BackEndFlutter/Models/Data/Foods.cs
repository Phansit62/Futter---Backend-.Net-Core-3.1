﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BackEndFlutter.Models.Data
{
    public partial class Foods
    {
        public Foods()
        {
            FoodOptions = new HashSet<FoodOptions>();
            ImageFood = new HashSet<ImageFood>();
            OderDetail = new HashSet<OderDetail>();
        }

        public int FoodId { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public bool? Status { get; set; }
        public int? CatefoodId { get; set; }
        public string Description { get; set; }

        public virtual CategoryFood Catefood { get; set; }
        public virtual ICollection<FoodOptions> FoodOptions { get; set; }
        public virtual ICollection<ImageFood> ImageFood { get; set; }
        public virtual ICollection<OderDetail> OderDetail { get; set; }
    }
}