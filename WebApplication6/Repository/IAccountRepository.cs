using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);

        Task<string> LoginAsync(SignInModel signInModel);

        Task<ApplicationUser> GetUser(string username);
    }
}
