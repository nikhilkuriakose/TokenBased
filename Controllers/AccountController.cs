using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Reflection;
using TokenBased.ViewModel;
using TokenBased.Repository;
using Microsoft.Extensions.Logging;

namespace TokenBased.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IRepository repository, ILogger<AccountController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Login(string returnUrl = "/")
        {
            _logger.Log(LogLevel.Information, "{0}:{1}: Login Action called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            LoginVM loginVM = new LoginVM();
            loginVM.ReturnURL = returnUrl;
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            _logger.Log(LogLevel.Information, "{0}:{1}: Username and Password submitted", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            /*Error Controller is created to handle all exceptions, So commenting try catch sections*/
            //try
            //{
                if (ModelState.IsValid)
                {
                    if ((loginVM.UserId = _repository.ValidateUser(loginVM)) is not null)
                    {
                        _logger.Log(LogLevel.Information, "{0}:{1}: Authentication Successful", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
                        List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginVM.UserId.ToString()),
                        new Claim(ClaimTypes.Email, loginVM.UserName)
                    };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    _logger.Log(LogLevel.Information, "{0}:{1}: Cookie generated successfully", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
                    return LocalRedirect(loginVM.ReturnURL);
                    }
                    else
                    {
                        ModelState.AddModelError("Invalid", "Your login attempt has failed. Make sure the username and password are correct.");
                        _logger.Log(LogLevel.Information, "{0}:{1}: Incorrect username or Password", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
                    }
                }
            //}
            //catch (NullReferenceException ex)
            //{
            //    _logger.Log(LogLevel.Error, "{0}:{1}: {2}", this.GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
            //    throw ex;
            //}
            //catch (Exception ex)
            //{
            //    _logger.Log(LogLevel.Error, "{0}:{1}: {2}", this.GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
            //}
            _logger.Log(LogLevel.Information, "{0}:{1}: User name or Password is blank", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            //try
            //{
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.Log(LogLevel.Information, "{0}:{1}: Logging out the user", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            //}
            //catch(Exception ex)
            //{
            //    _logger.Log(LogLevel.Error, "{0}:{1}: {2}", this.GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
            //}
            return LocalRedirect("/");
        }

    }
}
