using System.Threading.Tasks;

namespace RentACar.Services
{
    public interface IAuthService
    {
        //Task<bool> IsAuthorized();
        Task<bool> Authorize();
        Task SignOut();
    }
}
