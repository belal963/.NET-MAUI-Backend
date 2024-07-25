#if ANDROID
using Android.Widget;
#endif
using FindUsHere.General.Interfaces;
using FindUsHere.ModelMaui.Models;
using FindUsHere.ModelMaui.RestApi;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace FindUsHere.ViewModelMaui
{
    public class UserViewModel : ViewModelBase
    {

        private RestApiUser restApiUser;

        private Shell _shell;
        

       
        public UserViewModel(Shell shell)
        {
            restApiUser = new RestApiUser();
            _shell = shell;
           
        }

        public ObservableCollection<IUser> UserList { get; set; } = new ObservableCollection<IUser>();

        public async void InitializeUserAsync(string userName)
        {
            var user = await restApiUser.GetUserAsync(userName);
            foreach (var item in user)
            {
                UserList.Add(item);
            }



        }

        public async Task CreateFavourited()
        {
            if (await restApiUser.SendToFavourited(36, 2))
            {
                Message = "Ok";


            }

        }

        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _UserEmail;
        public string UserEmail
        {
            get
            {
                return _UserEmail;
            }
            set
            {
                _UserEmail= value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        private string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }




        private ICommand? _submitCommand;
        public ICommand? SubmitCommand
        {

            get
            {
                return _submitCommand ?? (_submitCommand = new Command(
                    async (p) =>
                    {
                        await InsertUserAsync();
                        
                    },
                    (p) =>
                    {
                        return true;
                    }
                    ));

            }


        }

        private ICommand? _submitLoginCommand;
        public ICommand? SubmitLoginCommand
        {
            get
            {
                return _submitLoginCommand ?? (_submitLoginCommand = new Command(
                    async (p) =>
                    {
                        await LoginUserAsync();
                        CheckInternet();

                    },
                    (p) =>
                    {
                        return true;
                    }));

            }
        }
        private ICommand? _submiUserUpdateCommand;
        public ICommand? SubmiUserUpdateCommand
        {
            get
            {
                return _submiUserUpdateCommand ?? (_submiUserUpdateCommand = new Command(
                    async (p) =>
                    {
                        await UpdateUserAsync();
                    },
                    (p) =>
                    {
                        return true;
                    }));
            }
        }
        private ICommand? _SubmitUserDeleteCommand;
        public ICommand? SubmitUserDeleteCommand
        {
            get
            {
                return _SubmitUserDeleteCommand ?? (_SubmitUserDeleteCommand = new Command(
                    async (p) =>
                    {
                        await DeleteUserAsync();
                    },
                    (p) =>
                    {
                        return true;
                    }));
            }
        }


        async Task CheckInternet()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
               
                
#if ANDROID
                Toast.MakeText(Platform.CurrentActivity, "Internet ready to go", ToastLength.Long).Show();
#endif     
            }else { Message = "No Internet"; }

            IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                Message = "there is WIFI";
            }
        }
        
        private async Task InsertUserAsync()
        {

            var responce = await restApiUser.PostUserAsync(UserName, Password, UserEmail);
            if (responce != null)
            {
                Message = "User is Created";

                Id = responce.Id;
                UserName = responce.UserName;
                UserEmail = responce.UserEmail; 
                Password = responce.Password;
                if (_shell != null)
                {
                    await Shell.Current.GoToAsync("SwipePageT");
                }
            }
        }


        private async Task LoginUserAsync()
        {
            var responce = await restApiUser.PostUserLoginAsync(UserName, Password);
            if (responce != null)
            {
                Message = "User already exist";

                Id = responce.Id;
                UserName = responce.UserName;
                UserEmail = responce.UserEmail;
                Password = responce.Password;

                if (_shell != null)
                {
                    await Shell.Current.GoToAsync("//firstPage");

                }
                else
                {
                    Message = "Error: User creation failed.";
                }
            }

        }

        private async Task UpdateUserAsync()
        {

            if (await restApiUser.PutUserAsync(UserName, Password, UserEmail))
            {
                Message = "User is Updated";
            }

        }
        private async Task DeleteUserAsync()
        {

            if (await restApiUser.DeleteUserAsync(Id))
            {
                Message = "User is Deleted";
                if(_shell != null)
                {
                    await _shell.GoToAsync("///Loginpage");
                }
                
            }

        }
    }
}
