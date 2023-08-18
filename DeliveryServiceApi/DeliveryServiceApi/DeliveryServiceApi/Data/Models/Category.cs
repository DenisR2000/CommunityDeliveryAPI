using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeliveryServiceApi.Data.Models;
using DeliveryServiceData.Data.Relations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeliveryServiceData.Data.Models
{
    public class Category
    {
        private readonly ILazyLoader _lazyLoader;

        public Category(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public Category()
        {
            Subcategorys = new HashSet<Subcategory>();
        }

        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } // спец Кроси, кросы найк, разпродажа

        public int? ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        private ICollection<Subcategory> _subcategorys;
        public virtual ICollection<Subcategory> Subcategorys
        {
            get => _lazyLoader.Load(this, ref _subcategorys);
            set => _subcategorys = value;
        }
    }
}

