using FindUsHere.Maui.Services;
using FindUsHere.ViewModelMaui;

namespace FindUsHere.Maui.View;

public partial class Loginpage : ContentPage
{
    private readonly AuthService _authService;

    public Loginpage(AuthService authService, UserViewModel userViewModel)
    {
        InitializeComponent();
        _authService = authService;
        BindingContext = userViewModel;
    }

    private async void OnMainPageClicked(object sender, EventArgs e)
    {
        _authService.Login();
        await Shell.Current.GoToAsync($"{nameof(MainPage)}");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(RegisterUserPage)}");
    }
}
