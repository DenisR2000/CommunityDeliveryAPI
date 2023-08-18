using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceData.Data.Relations;

namespace DeliveryServiceData.Data.Models
{
	public class Category
	{
		public Category()
		{
			GoodCategory = new HashSet<GoodCategory>();
		}
		[Key]
		public int CategoryId { get; set; }
		[Required]
		public string? CategoryName { get; set; }
		public virtual ICollection<GoodCategory> GoodCategory { get; set; }
	}
}

