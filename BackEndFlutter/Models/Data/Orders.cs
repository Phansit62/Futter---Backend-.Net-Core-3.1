﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BackEndFlutter.Models.Data
{
    public partial class Orders
    {
        public Orders()
        {
            OderDetail = new HashSet<OderDetail>();
            Payments = new HashSet<Payments>();
        }

        public int OrderId { get; set; }
        public DateTime? DateIn { get; set; }
        public int? TableId { get; set; }
        public int? Total { get; set; }
        public int? StatusId { get; set; }

        public virtual StatusOrder Status { get; set; }
        public virtual Tables Table { get; set; }
        public virtual ICollection<OderDetail> OderDetail { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }
}