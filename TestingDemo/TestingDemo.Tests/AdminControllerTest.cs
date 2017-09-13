using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingDemo.Controllers;
using TestingDemo.Models;
using System.Web;

namespace TestingDemo.Tests
{
    [TestClass]
    class AdminControllerTest
    {
        [TestMethod]
        public void CanChangeLoginName()
        {
            User user = new User() { LoginName = "Bogdan" };
            FakeRepository repositoryParam = new FakeRepository();
            repositoryParam.AddUser(user);
            AdminController target = new AdminController(repositoryParam);
            string oldLoginParam = user.LoginName;
            string newLoginParam = "Janek";

            target.ChangeLoginName(oldLoginParam, newLoginParam);

            Assert.AreEqual(newLoginParam, user.LoginName);
            Assert.IsTrue(repositoryParam.DidSubmitChanges);


        }
    }

    internal class FakeRepository : IUserRepository
    {

        public List<User> users = new List<User>();
        public bool DidSubmitChanges = false;
        public FakeRepository()
        {
        }

        public void AddUser(User user)
        {
            users.Add(user); //throw new NotImplementedException();
        }

        public User FetchByLoginName(string loginName)
        {
            return users.First(m => m.LoginName == loginName);
        }

        public void SubmitChanges()
        {
            //throw new NotImplementedException();
            DidSubmitChanges = true;
        }
    }
}
