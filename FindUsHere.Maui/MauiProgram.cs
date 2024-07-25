using FindUsHere.Maui.Services;
using FindUsHere.Maui.View;
using FindUsHere.ViewModelMaui;
using Microsoft.Extensions.Logging;

namespace FindUsHere.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 5 Free-Solid-900.otf", "Fa-Solid");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddTransient<Loginpage>();
            builder.Services.AddTransient<UpdateUser>();
            builder.Services.AddTransient<DeleteUserPage>();
            builder.Services.AddTransient<RegisterUserPage>();
            builder.Services.AddSingleton<UserViewModel>();
            builder.Services.AddSingleton<BusinessViewModel>();
            builder.Services.AddTransient<Shell>();
            //builder.Services.AddTransient<BusinessPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<SettingsPage>();
            //builder.Services.AddTransient<FavLikedDisLiked>();
           

            return builder.Build();
        }
    }
}
