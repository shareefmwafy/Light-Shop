﻿using Light_Shop.API.Models;

namespace Light_Shop.API.DTOs.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string MainImage { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
