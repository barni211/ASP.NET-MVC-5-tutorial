using TestingDemo.Models;

namespace TestingDemo.Controllers
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User FetchByLoginName(string loginName);
        void SubmitChanges();
    }
}