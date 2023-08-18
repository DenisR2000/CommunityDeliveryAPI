using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeliveryServiceApi.Data.Contexts;
using DeliveryServiceApi.Data.Models;
using DeliveryServiceApi.Data.Models.ViewModels;
using DeliveryServiceApi.JWTConfig;
using DeliveryServiceApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DeliveryServiceApi.Controllers.ApiControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.UserName };
                                    var addedUser = await _userManager.FindByNameAsync(model.UserName);
                if (addedUser != null)
                {
                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        List<string> role = new List<string>();
                        role.Add("User");
                        await _userManager.AddToRolesAsync(user, role);
                        await _signInManager.SignInAsync(user, false);

                        var identity = await GetIdentity(addedUser.Email, model.Password);
                        if (identity == null)
                        {
                            return BadRequest("Invalid login or password");
                        }
                        var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSURER,
                            audience: AuthOptions.AUDIENCE,
                            claims: identity.Claims,
                            expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                            );
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                        return Json(new
                        {
                            access_token = encodedJwt,
                            userId = addedUser.Id,
                            roles = await _userManager.GetRolesAsync(addedUser)
                        });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return BadRequest("BadRequest");
                    }
                }
                else
                {
                    return new BadRequestObjectResult("Invalid login or password");
                }
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                string UserName = model.UserName.Replace(" ", "");
                string Password = model.Password.Replace(" ", "");

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var identity = await GetIdentity(user.Email, model.Password);
                    if (identity == null)
                    {
                        return BadRequest("Invalid login or password");
                    }
                    var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSURER,
                        audience: AuthOptions.AUDIENCE,
                        claims: identity.Claims,
                        expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                        );
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                    return Json(new
                    {
                        access_token = encodedJwt,
                        userId = user.Id,
                        roles = await _userManager.GetRolesAsync(user)
                    });
                }
                else
                {
                    return new BadRequestObjectResult("Invalid login or password");
                }
            }
            catch (Exception ex)
            {
            }
            return new OkObjectResult("Invalid login or password");
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else
            {
                ModelState.AddModelError("", "Invalid login or password");
            }
            return null;
        }

        [HttpPost("check-email")]
        public async Task<IActionResult> CheckEmail(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return Ok();
            }
            return NotFound("User not found");
        }

        [HttpPost("check-user-by-eamil")]
        public async Task<IActionResult> CheckUserByEmailAsync(ChangePasswordViewModel model) // для скидання паролю через пошту
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var confirmationLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
                //EmailTwoFactor emailHelper = new EmailTwoFactor();
                bool emailResponse = MailService.SendEmailTwoFactorCode(user.Email, confirmationLink, "Confirm password changing", "Click on link to confirm password changing");
                if (emailResponse)
                {
                    return Ok("We sent an email on this address, check your messages and click on link to reset your password");
                }
                else
                {
                    return StatusCode(500, "Email sending error");
                }
            }
            return NotFound("Such user doesn't exist");
        }

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ChangePasswordViewModel model) // для скидання паролю
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user != null)
        //    {
        //        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        //        if (result.Succeeded)
        //            return Ok("Password changed successfully");
        //        else
        //        {
        //            return BadRequest("Invalid token");
        //        }
        //    }
        //    return BadRequest("Password wasn't changed");
        //}

        //[HttpPost]
        //public async Task<IActionResult> ConfirmPasswordChanging(string token, string email, string oldPassword, string newPassword)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user == null)
        //        return BadRequest(new { error = "Error" });
        //    var result = await _userManager.ResetPasswordAsync(user, token, oldPassword);
        //    if (result.Succeeded)
        //    {
        //        var passwordChangeResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        //        if (passwordChangeResult.Succeeded)
        //        {
        //            //return Ok(new { message = "Password was changed" });
        //            return Redirect($"http://localhost:44364/userdatassettings/success");

        //        }
        //        else
        //        {
        //            //return BadRequest(new { error = passwordChangeResult.Errors });
        //            var error = passwordChangeResult.Errors.ToList().FirstOrDefault().Description;
        //            return Redirect($"http://localhost:44364/userdatassettings/{error}");
        //        }


        //    }
        //    else
        //    {
        //        //return BadRequest(new { error = "Password wasn't changed" });
        //        return Redirect($"http://localhost:44364/userdatassettings/failure");

        //    }
        //    //return Redirect("http://localhost:44364/");
        //}
    }
}

