using System;
using Microsoft.AspNetCore.Identity;

namespace DeliveryServiceApi.Data.Models.ViewModels
{
	public class ChangeRoleViewModel
	{
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public UserManager<User> UserManager { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel(UserManager<User> userManager)
        {
            UserManager = userManager;
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}

