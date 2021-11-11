using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using TokenBased.Models;
using TokenBased.ViewModel;
using Microsoft.Extensions.Logging;

namespace TokenBased.Repository
{
    public class DummyRepository : IRepository
    {
        private readonly ILogger<DummyRepository> _logger;
        public DummyRepository(ILogger<DummyRepository> logger)
        {
            _logger = logger;
        }
        public List<User> GetAllUsers()
        {
            //List<User> users = null;
            _logger.Log(LogLevel.Information, "{0}:{1}: GetAllUsers called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            try
            {
                List<User> users = new List<User>()
            {
                new User
                {
                    FirstName = "Sam",
                    LastName = "Thomas",
                    UserId = 1200
                },
                new User
                {
                    FirstName = "Ajith",
                    LastName = "Nair",
                    UserId = 1000
                },
                new User
                {
                    FirstName = "Kaliyan",
                    LastName = "Balu",
                    UserId = 666
                },
                new User
                {
                    FirstName = "Kaliyan",
                    LastName = "Kaliyan",
                    UserId = 5000
                }
            };
                if (users is not null)
                {
                    users.Sort((x, y) => x.UserId.CompareTo(y.UserId));
                }
                return users;
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, "{0}:{1}: {2}", this.GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }

        public int? ValidateUser(LoginVM loginVM)
        {
            _logger.Log(LogLevel.Information, "{0}:{1}: ValidateUser called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            //In realworld this validation will done at DB

            if (loginVM is not null)
            {
                int i = 367;
                if (loginVM.UserName == "celtrino" && loginVM.Password == "celtrino")
                    return i;
            }
            return null;
        }
    }
}
