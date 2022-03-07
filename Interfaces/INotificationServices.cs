using System.Threading.Tasks;

namespace Interfaces
{
    public interface INotificationServices
    {
        Task SendNewUserCreation(string userName, string email);
    }
}
