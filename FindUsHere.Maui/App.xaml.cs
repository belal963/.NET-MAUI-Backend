using FindUsHere.Maui.View;

namespace FindUsHere.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //MainPage = new NavigationPage(new DeleteUserPage());
        }
    }
}
