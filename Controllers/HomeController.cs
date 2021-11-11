using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenBased.Repository;
using TokenBased.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace TokenBased.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            //try
            //{
                _logger.Log(LogLevel.Information, "{0}:{1}: Index Action called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
                List<User> users = _repository.GetAllUsers();
                return View(users);
            //}
            //catch(Exception ex)
            //{
            //    _logger.Log(LogLevel.Error, "{0}:{1}: {2}", this.GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
            //    return null;
            //}
        }
        [Authorize]
        public IActionResult DemoMenu()
        {
            _logger.Log(LogLevel.Information, "{0}:{1}: DemoMenu Action called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            return View();
        }
        [Authorize]
        public IActionResult IndexNull()
        {
            _logger.Log(LogLevel.Information, "{0}:{1}: IndexNull Action called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            //Intentially Assigning null
            List<User> users = null;
            return View("Index", users);
        }
        [Authorize]
        public IActionResult IndexException()
        {
            _logger.Log(LogLevel.Information, "{0}:{1}: IndexException Action called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            List<User> users = null;
            //Intentially throwing an exception
            throw new Exception("For Demo purpose");
            return View("Index",users);
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
