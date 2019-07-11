using System.Threading.Tasks;
using Firebase.Xamarin.Auth;

namespace Firebase.sample.Services
{
    public interface IFirebaseAuthService
    {
        FirebaseAuthLink AuthLink { get; }

        Task<bool> LoginAsync(string email, string password);
    }
}