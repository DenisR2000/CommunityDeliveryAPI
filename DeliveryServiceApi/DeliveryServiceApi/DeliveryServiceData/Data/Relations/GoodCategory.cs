using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceData.Data.Models;

namespace DeliveryServiceData.Data.Relations
{
	public class GoodCategory
	{
		[Key]
		public int Id { get; set; }
		public int GoodId { get; set; }
		public Good Good { get; set; }
		public int CategoryId { get; set; }
        public Category Category { get; set; }
	}
}

