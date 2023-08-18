using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DeliveryServiceApi.Data.Models.ViewModels
{
	public class UserViewModel
	{
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public string ResetPasswordToken { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public UserManager<User> UserManager { get; set; }

        public IList<string> UserRoles { get; set; }
        public string CurentEditor { get; set; }

        public UserViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}

