﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeliveryServiceApi.Data.Models;

namespace DeliveryServiceData.Data.Models.Products
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public byte[]? Bytes { get; set; }
        public string? Description { get; set; }
        public string? FileExtension { get; set; }
        public decimal? Size { get; set; }
        
        public int? GoodId { get; set; }
        public Good Good { get; set; }

        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}

