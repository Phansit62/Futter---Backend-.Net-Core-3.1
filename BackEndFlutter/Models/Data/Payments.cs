﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace BackEndFlutter.Models.Data
{
    public partial class Payments
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public DateTime? DateIn { get; set; }
        public string Image { get; set; }
        public bool? Status { get; set; }
        public int? OrderDeliveryId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual OrdersDelivery OrderDelivery { get; set; }
    }
}