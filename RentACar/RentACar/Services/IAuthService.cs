using System.Threading.Tasks;

namespace RentACar.Services
{
    public interface IAuthService
    {
        Task Authorize();
        void SignOut();
    }
}
