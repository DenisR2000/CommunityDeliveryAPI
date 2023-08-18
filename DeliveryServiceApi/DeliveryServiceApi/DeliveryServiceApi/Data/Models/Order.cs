using System;
using System.ComponentModel.DataAnnotations;
using DeliveryServiceData.Data.Models;

namespace DeliveryServiceApi.Data.Models
{
	public class Order
	{
		public Order()
		{
            Goods = new HashSet<Good>();
        }

        [Key]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public int? GoodId { get; set; }
        public virtual ICollection<Good> Goods { get; set; }
    }
}

