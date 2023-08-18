using System;
using DeliveryServiceData.Data.Models;
using DeliveryServiceData.Data.Models.Products;
using DeliveryServiceData.Data.Relations;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceData.Data.Contexts
{
	public class AppDeliveryContext : DbContext
	{
		public AppDeliveryContext(DbContextOptions<AppDeliveryContext> options) :
			base(options)
		{
			
		}

        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<GoodCategory> GoodCategories { get; set; }
    }
}

