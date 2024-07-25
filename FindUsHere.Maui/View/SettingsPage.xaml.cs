using FindUsHere.ViewModelMaui;

namespace FindUsHere.Maui.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(UserViewModel userViewModel)
	{
		InitializeComponent();
		BindingContext = userViewModel;	
	}

    private async void NewBusiness(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync($"{nameof(BusinessPage)}");
    }
    private async void Edit_DeleteBusiness(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync($"{nameof(EditBusiness)}");
    }
    private async void Edit_DeleteUser(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(UpdateUser)}");
    }

}