using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceData.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeliveryServiceApi.Data.Models
{
	public class Department
    {
        private readonly ILazyLoader _lazyLoader;
        public Department()
		{
            Categories = new HashSet<Category>();
		}

        public Department(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        [Key]
        public int Id { get; set; }
		public string? Name { get; set; }

        private ICollection<Category> _categories; 
        public virtual ICollection<Category> Categories
        {
            get => _lazyLoader.Load(this, ref _categories);
            set => _categories = value;
        }
    }
}

