using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Numerics;
using System.Reflection;
using DeliveryServiceApi.Data.Models;
using DeliveryServiceData.Data.Models.Products;
using DeliveryServiceData.Data.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeliveryServiceData.Data.Models
{
    public class Good
    {
        private readonly ILazyLoader _lazyLoader;
        public Good(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public Good()
        {
            Photos = new HashSet<Photo>();
        }

        [Key]
        public int GoodId { get; set; }
        [MaxLength(250)]
        public string Manufacturer { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        public string? Model { get; set; }
        [MaxLength(250)]
        public string? Country { get; set; } // Происхождение
        [Required]
        public double Price { get; set; }
        public int Rate { get; set; }
        public int? SalePersent { get; set; }
        public string? Material { get; set; }
        public string? Gender { get; set; }
        public string? FabricСontent { get; set; }//Ткань содержание (51% (включительно)-70% (включительно))
        public string? Season { get; set; } // Весна / лето
        public string? FabricDensity { get; set; } // Плотность ткани
        public string? ClothesPatterns { get; set; } // Одежда узоры (SLIM)
        public string? Size { get; set; } // M S L XXL
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? Guarantee { get; set; }
        public string? CompleteSet { get; set; } 
        public string? GoodCode { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

        public int? CategotyTypeId { get; set; }
        public CategoryType CategoryType { get; set; }

        private ICollection<Photo> _photos;
        public virtual ICollection<Photo> Photos
        {
            get => _lazyLoader.Load(this, ref _photos);
            set => _photos = value;
        }
    }
}

