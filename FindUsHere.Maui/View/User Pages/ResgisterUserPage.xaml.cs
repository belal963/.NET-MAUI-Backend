using FindUsHere.ViewModelMaui;

namespace FindUsHere.Maui.View;

public partial class RegisterUserPage : ContentPage
{
    public RegisterUserPage(UserViewModel userViewModel)
    {
        InitializeComponent();
        BindingContext = userViewModel;
    }

    private async void OnMainPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Loginpage");
    }
}
