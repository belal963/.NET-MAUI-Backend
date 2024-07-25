using FindUsHere.Maui.Services;

namespace FindUsHere.Maui.View;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService _authService;
    public LoadingPage(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (await _authService.IsAuthenticedAsync())
        {

            // redirect to main Page
            await Shell.Current.GoToAsync("///FirstLoginpage");

        }
        else
        {

            // rediect to login 
            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }




    }
}