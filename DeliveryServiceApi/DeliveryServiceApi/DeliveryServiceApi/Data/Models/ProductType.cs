using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceApi.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeliveryServiceData.Data.Models
{
	public class ProductType
    {
        private readonly ILazyLoader _lazyLoader;
        public ProductType(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public ProductType()
        {
            CategoriesType = new HashSet<CategoryType>();
            Categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        private ICollection<CategoryType> _categoriesType;
        public virtual ICollection<CategoryType> CategoriesType
        {
            get => _lazyLoader.Load(this, ref _categoriesType);
            set => _categoriesType = value;
        }

        private ICollection<Category> _categories;
        public virtual ICollection<Category> Categories
        {
            get => _lazyLoader.Load(this, ref _categories);
            set => _categories = value;
        }
    }
}

