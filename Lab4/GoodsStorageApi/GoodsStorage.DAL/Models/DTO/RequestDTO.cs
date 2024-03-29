﻿using GoodsStorage.DAL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class RequestDTO
    {
        public Guid Id { get; set; }
        public Guid GoodId { get; set; }
        public string GoodName { get; set; }
        public string CustomerId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal ExpectedPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
