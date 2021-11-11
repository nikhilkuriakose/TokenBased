using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TokenBased.Models;
using TokenBased.ViewModel;

namespace TokenBased.Repository
{
    public class AppSettingsRepository : IRepository
    {
        private readonly IConfiguration _cofiguration;
        private readonly ILogger<AppSettingsRepository> _logger;
        public AppSettingsRepository(IConfiguration configuration, ILogger<AppSettingsRepository> logger)
        {
            _cofiguration = configuration;
            _logger = logger;
        }
        public List<User> GetAllUsers()
        {
            _logger.Log(LogLevel.Information, "{0}:{1}: GetAllUsers called", this.GetType().FullName, MethodBase.GetCurrentMethod().Name);
            try
            {
                List<User> users = _cofiguration.GetSection("Employees").Get<List<User>>();
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
            try
            {
                LoginVM fromFile = _cofiguration.GetSection("UserAccount").Get<LoginVM>();
                if (loginVM is not null)
                {
                    if (loginVM.UserName == fromFile.UserName && loginVM.Password == fromFile.Password)
                        return fromFile.UserId;
                }
                return null;
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, "{0}:{1}: {2}", this.GetType().FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
                return null;
            }
        }
    }
}
