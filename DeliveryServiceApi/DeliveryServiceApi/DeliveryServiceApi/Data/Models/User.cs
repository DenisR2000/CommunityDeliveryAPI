using System;
using Microsoft.AspNetCore.Identity;

namespace DeliveryServiceApi.Data.Models
{
	public class User : IdentityUser
	{
        public string? LastName { get; set; }

		public virtual IList<Order> Orders { get; set; }
	}
}

