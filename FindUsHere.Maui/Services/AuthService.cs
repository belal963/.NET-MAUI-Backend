using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.Maui.Services
{
    public class AuthService
    {

        private const string AuthStateKey = "AuthState";
        private bool IsAUser;
        public async Task<bool> IsAuthenticedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get<bool>(AuthStateKey, true);

            return authState;
        }

        public void Login()
        {
            Preferences.Default.Set<bool>(AuthStateKey, true);  
        }

        public void Logout()
        {
            Preferences.Default.Remove(AuthStateKey);
        }

        public void ChenkIsAUser(bool isAUser) 
        {
        
        }
    }
}
