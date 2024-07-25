using FindUsHere.Maui.View;

namespace FindUsHere.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Loginpage), typeof(Loginpage));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(RegisterUserPage), typeof(RegisterUserPage));
            //Routing.RegisterRoute(nameof(BusinessPage), typeof(BusinessPage));
            //Routing.RegisterRoute(nameof(FavLikedDisLiked), typeof(FavLikedDisLiked));  
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));  
            //Routing.RegisterRoute(nameof(EditBusiness), typeof(EditBusiness));
            Routing.RegisterRoute(nameof(UpdateUser), typeof(UpdateUser));  

        }
    }
}
