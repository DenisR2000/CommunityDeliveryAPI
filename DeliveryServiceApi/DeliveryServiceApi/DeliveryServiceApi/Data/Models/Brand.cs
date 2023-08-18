using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceData.Data.Models;
using DeliveryServiceData.Data.Models.Products;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeliveryServiceApi.Data.Models
{
	public class Brand
	{
        private readonly ILazyLoader _lazyLoader;

        public Brand(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public Brand()
        {
            Goods = new HashSet<Good>();
            Departments = new HashSet<Department>();
            Photos = new HashSet<Photo>();
        }

        [Key]
		public int BrandId { get; set; }
		public string? BrandName { get; set; }
        public string? Description { get; set; }

        private ICollection<Good> _goods;
        public virtual ICollection<Good> Goods
        {
            get => _lazyLoader.Load(this, ref _goods);
            set => _goods = value;
        }

        private ICollection<Department> _departments;
        public virtual ICollection<Department> Departments
        {
            get => _lazyLoader.Load(this, ref _departments);
            set => _departments = value;
        }

        private ICollection<Photo> _photos;
        public virtual ICollection<Photo> Photos
        {
            get => _lazyLoader.Load(this, ref _photos);
            set => _photos = value;
        }
    }
}

