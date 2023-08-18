using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceData.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DeliveryServiceApi.Data.Models
{
	public class Subcategory
	{
		public Subcategory()
		{
            Goods = new HashSet<Good>();
        }

        private readonly ILazyLoader _lazyLoader;

        public Subcategory(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        private ICollection<Good> _goods;
        public virtual ICollection<Good> Goods
        {
            get => _lazyLoader.Load(this, ref _goods);
            set => _goods = value;
        }
    }
}

