using Firebase.sample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.sample.Services.Implementation
{
    public class PlacesService : FirebaseDataStore<Place>
    {
        public PlacesService(IFirebaseAuthService authService, string path)
            : base(authService, path)
        {
        }
    }
}
