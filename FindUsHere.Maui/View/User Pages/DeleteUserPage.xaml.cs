using FindUsHere.Maui.Services;
using FindUsHere.ViewModelMaui;

namespace FindUsHere.Maui.View;

public partial class DeleteUserPage : ContentPage
{

    private readonly AuthService _authService;
	public DeleteUserPage(AuthService authService, UserViewModel userViewModel)
	{
		InitializeComponent();
        _authService = authService;
        BindingContext = userViewModel;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        _authService.Logout();
        Shell.Current.GoToAsync("///Loginpage");
    }
}