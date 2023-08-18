using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryServiceApi.Data.Models.ViewModels
{
	public class ChangePasswordViewModel
	{
        [EmailAddress]
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}

