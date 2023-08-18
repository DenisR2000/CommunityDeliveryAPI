using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceData.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeliveryServiceApi.Data.Models
{
	public class CategoryType
    {
        private readonly ILazyLoader _lazyLoader;

        public CategoryType(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public CategoryType()
		{
            Goods = new HashSet<Good>();
        }

		[Key]
		public int Id { get; set; }
		public string Name { get; set; } // Обувь сумки тд...

        public int? ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        private ICollection<Good> _goods;
        public virtual ICollection<Good> Goods
        {
            get => _lazyLoader.Load(this, ref _goods);
            set => _goods = value;
        }
    }
}

