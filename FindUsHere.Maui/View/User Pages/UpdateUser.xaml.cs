using FindUsHere.ViewModelMaui;

namespace FindUsHere.Maui.View;

public partial class UpdateUser : ContentPage
{
	public UpdateUser(UserViewModel userViewModel)
	{
		InitializeComponent();
		BindingContext = userViewModel;
    }
}