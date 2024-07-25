using FindUsHere.General.Interfaces;
using FindUsHere.Model.Models;
using FindUsHere.Model.RestApi;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace FindUsHere.ViewModel
{
    public class UserViewModel : ViewModelBase
    {

        private RestApiUser restApiUser;
        private string userName = "Klaus";

        public ICommand SubmitCommandMaui { get; set; }

        public UserViewModel()
        {
            restApiUser = new RestApiUser();

            //UserValues();
            //InitializeUserAsync(userName);


            //SubmitCommandMaui = new Command(InsertUserAsync);

        }


        //private void UserValues()
        //{
        //    Id = 23;
        //    UserName = "Res";
        //    UserEmail = "goodfood.com";
        //    Password = "viewmodel";


        //}

        public ObservableCollection<IUser> UserList { get; set; } = new ObservableCollection<IUser>();

        public async void InitializeUserAsync(string userName)
        {
            var user = await restApiUser.GetUserAsync(userName);
            foreach (var item in user)
            {
                UserList.Add(item);
                //item.UserName = UserName;
                //item.UserEmail = UserEmail;
                //item.Id = Id;
                

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
                OnPrpertyChanged(nameof(Id));
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
                OnPrpertyChanged(nameof(UserName));
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
                OnPrpertyChanged(nameof(UserEmail));
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
                OnPrpertyChanged(nameof(Password));
            }
        }


        

        private ICommand? _submitCommand;
        public ICommand? SubmitCommand
        {

            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand(
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

        private ICommand? _submitCommand2;
        public ICommand? SubmitCommand2
        {

            get
            {
                return _submitCommand2 ?? (_submitCommand2 = new RelayCommand(
                     (p) =>
                    {
                        InitializeUserAsync(userName);

                    },
                    (p) =>
                    {

                        return true;
                    }


                    ));

            }


        }



        private async Task InsertUserAsync()
        {
            IUser user = new User();
            user.UserName = _UserName;  
            user.Password = _Password;
            user.UserEmail = _UserEmail;
            user.Id = Id;
            await restApiUser.PostUserAsync(user);    



        }
    }
}
