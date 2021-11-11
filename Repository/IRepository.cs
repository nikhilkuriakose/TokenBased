using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenBased.ViewModel;
using TokenBased.Models;

namespace TokenBased.Repository
{
    public interface IRepository
    {
        public int? ValidateUser(LoginVM loginVM);
        public List<User> GetAllUsers();
    }
}
