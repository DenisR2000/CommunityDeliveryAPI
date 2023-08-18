using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DeliveryServiceApi.Data.Models;
using DeliveryServiceApi.Data.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryServiceApi.Controllers.ApiControllers
{
    [Authorize(Roles = "MainAdmin")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("create-user")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User { Email = model.UserEmail, UserName = model.UserName, LastName = model.LastName };
                IdentityResult result = await _userManager.CreateAsync(newUser);

                await _userManager.AddPasswordAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(newUser);
                    // получаем список ролей, которые были добавлены
                    var addedRoles = model.UserRoles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(model.UserRoles);

                    await _userManager.AddToRolesAsync(newUser, addedRoles);

                    await _userManager.RemoveFromRolesAsync(newUser, removedRoles);

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("CreateUser", "User");
                }
            }
            else
            {
                var allRoles = _roleManager.Roles.ToList();
                return View(new UserViewModel { UserName = model.UserName, LastName = model.LastName, UserEmail = model.UserEmail, Password = model.Password, AllRoles = allRoles });
            }
        }

        [HttpPost("delete-user")]
        [Authorize(Roles = "MainAdmin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.Id == Id);

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("edit-user/{userId}/{role}")]
        public async Task<IActionResult> EditUser(string userId, string role = "User")
        {
            User user = await _userManager.FindByIdAsync(userId);
            var userRole = await _userManager.GetRolesAsync(user);
            string resultRole = userRole.FirstOrDefault(x => x == "MainAdmin");
            UserViewModel model = new UserViewModel();
            if (user != null)
            {
                if (resultRole == "MainAdmin")
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    model.UserId = user.Id;
                    model.UserName = user.UserName;
                    model.LastName = user.LastName;
                    model.UserEmail = user.Email;
                    model.UserRoles = userRoles;
                    model.AllRoles = allRoles;
                    model.UserManager = _userManager;
                    model.CurentEditor = "MainAdmin";
                }
                else
                {
                    model.UserId = user.Id;
                    model.UserName = user.UserName;
                    model.LastName = user.LastName;
                    model.UserEmail = user.Email;
                    model.UserManager = _userManager;
                }

                return View(model);
            }
            return NotFound();
        }

        [HttpPost("edit-user")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            User user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.LastName = model.LastName;
                user.Email = model.UserEmail;
                if (!String.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    string resultRole = userRole.FirstOrDefault(x => x == "MainAdmin");
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles != null)
                    {
                        try
                        {
                            // получаем список ролей, которые были добавлены
                            var addedRoles = model.UserRoles.Except(userRoles);
                            // получаем роли, которые были удалены
                            var removedRoles = userRoles.Except(model.UserRoles);

                            await _userManager.AddToRolesAsync(user, addedRoles);

                            await _userManager.RemoveFromRolesAsync(user, removedRoles);
                            if (resultRole == "MainAdmin")
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                return RedirectToAction("UserAccount", "User");
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return NotFound();
        }
    }
}

