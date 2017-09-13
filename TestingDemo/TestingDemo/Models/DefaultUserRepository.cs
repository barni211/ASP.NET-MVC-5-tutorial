using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingDemo.Controllers;

namespace TestingDemo.Models
{
    public class DefaultUserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User FetchByLoginName(string loginName)
        {
            return new User() { LoginName = loginName };
            //throw new NotImplementedException();
        }

        public void SubmitChanges()
        {
            throw new NotImplementedException();
        }
    }
}