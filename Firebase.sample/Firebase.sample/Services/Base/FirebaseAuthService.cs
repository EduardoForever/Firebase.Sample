using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.sample.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private FirebaseAuthLink _authLink;

        public FirebaseAuthLink AuthLink
        {
            get
            {
                if (this._authLink == null)
                {
                    throw new ArgumentNullException("First you need to log-in to Firebase");
                }

                return this._authLink;
            }
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Config.ApiKeys.FirebaseApiKey));

            this._authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

            return this._authLink != null && this._authLink.FirebaseToken != null;
        }
    }
}
