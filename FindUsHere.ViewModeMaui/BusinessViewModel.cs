using FindUsHere.General;
using FindUsHere.General.Interfaces;
using FindUsHere.ModelMaui.Models;
using FindUsHere.ModelMaui.RestApi;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Timer = System.Timers.Timer;

namespace FindUsHere.ViewModelMaui
{
    public class BusinessViewModel : ViewModelBase
    {
        #region Variables
        public bool UploadedFile = false;
        public long TotalBytes;
        public long UploadedBytes;
        private int currentIndex = 0;
        private int UserId;
        private Timer countdownTimer;
        
        #endregion

        #region Lists
        public List<string> Photolinklist { get; set; }
        public List<Hours2> thenewlist;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<BusinessInfo> BusinessInfosByIdlist { get; set; }

        public ObservableCollection<BusinessInfo> FavLikedDislikedBusinessList { get; set; }
        public ObservableCollection<BusinessInfoWithFirstPhoto> BusinessInfosWithFirstPhoto { get; set; }

        public ObservableCollection<BusinessInfoWithFirstPhoto> FavLikedDislikedBusinessPhotoList { get; set; }
        public ObservableCollection<BusinessInfo> BusinessInfo { get; set; }
        public ObservableCollection<string> PhotosLinkUI { get; set; }
        #endregion


        #region Others
        private RestApiBusiness restApiBusiness;
        private RestApiUser restApiUser;
        private UserViewModel userViewModel;
        private readonly Shell _shell;
        public ICommand RefreshCommand { get; }
        public ICommand FrameTappedCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand GoBusiness { get; }

        public ICommand DeleteBusinessCommand { get; }

        public enum Direction{Left, Right, Up}

        #endregion

        #region ctor

        public BusinessViewModel(Shell shell, UserViewModel userViewModel)
        {
            _shell = shell;
            BusinessInfo = new ObservableCollection<BusinessInfo>();
            restApiBusiness = new RestApiBusiness();
            restApiUser = new RestApiUser();
            Hours = new List<Hours>();
            thenewlist = new List<Hours2>();
            Categories = new ObservableCollection<Category>();
            BusinessInfosByIdlist = new ObservableCollection<BusinessInfo>();
            Photolinklist = new List<string>();
            PhotosLinkUI = new ObservableCollection<string>();
            FavLikedDislikedBusinessList = new ObservableCollection<BusinessInfo>();
            FavLikedDislikedBusinessPhotoList = new ObservableCollection<BusinessInfoWithFirstPhoto>();
            FrameTappedCommand = new Command(OnFrameTapped);
            DeleteCommand = new Command<int>(OnDeleteCommand);
            GoBusiness = new Command(GoToBusiness);
            DeleteBusinessCommand = new Command(DeleteBusiness);
            GetCategories();
            GetBusinessForSwipe();
            this.userViewModel = userViewModel;
            GetUserBusiness();
            UserId = userViewModel.Id;
            BusinessInfosWithFirstPhoto = new ObservableCollection<BusinessInfoWithFirstPhoto>();
            RefreshCommand = new Command(ExecuteRefreshCommand);
            PopulateBusinessInfosWithFirstPhoto();
            TimeLeft = new TimeSpan(1, 0, 0);

        }

        #endregion


        #region BusinessInfo Attributes 

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

        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _FirstPhotoLink;
        public string FirstPhotoLink
        {
            get
            {
                return _FirstPhotoLink;
            }
            set
            {
                _FirstPhotoLink = value;
                OnPropertyChanged(nameof(FirstPhotoLink));
            }
        }

        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            { _Description = value;
            OnPropertyChanged(nameof(Description));}
        }

        private string _PhoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }


        private string _Website;
        public string Website
        {
            get
            {
                return _Website;
            }
            set
            {
                _Website = value;
                OnPropertyChanged(nameof(Website));
            }
        }

        private int _PostCode;
        public int PostCode
        {
            get
            {
                return _PostCode;
            }
            set
            {
                _PostCode = value;
                OnPropertyChanged(nameof(PostCode));
            }
        }
        private string _City;
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
                OnPropertyChanged(nameof(City));
            }
        }
        private string _Street;
        public string Street
        {
            get
            {
                return _Street;
            }
            set
            {
                _Street = value;
                OnPropertyChanged(nameof(Street));
            }
        }

        private string _HouseNumber;
        public string HouseNumber
        {
            get
            {
                return _HouseNumber;
            }
            set
            {
                _HouseNumber = value;
                OnPropertyChanged(nameof(HouseNumber));
            }
        }

        
        private string _Addition;
        public string Addition
        {
            get
            {
                return _Addition;
            }
            set
            {
                _Addition = value;
                OnPropertyChanged(nameof(Addition));
            }
        }

        private string _Category;
        public string Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        private double _GpsLongitude;
        public double GpsLongitude
        {
            get
            {
                return _GpsLongitude;
            }
            set
            {
                _GpsLongitude = value;
                OnPropertyChanged(nameof(GpsLongitude));
            }
        }
        private double _GpsLatitude;
        public double GpsLatitude
        {
            get
            {
                return _GpsLatitude;
            }
            set
            {
                _GpsLatitude = value;
                OnPropertyChanged(nameof(GpsLatitude));
            }
        }

        private double _GpsAltitude;
        public double GpsAltitude
        {
            get
            {
                return _GpsAltitude;
            }
            set
            {
                _GpsAltitude = value;
                OnPropertyChanged(nameof(GpsAltitude));
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


        #endregion

        #region .

        private TimeSpan _TimeLeft;

        public TimeSpan TimeLeft
        {
            get { return _TimeLeft; }
            set
            {
                if (_TimeLeft != value)
                {
                    _TimeLeft = value;
                    OnPropertyChanged(nameof(TimeLeft));

                    if (countdownTimer == null)
                    {
                        StartTimer();
                    }
                }
            }
        }

        private string _OpenOrColse;
        public string OpenOrColse
        {
            get
            {
                return _OpenOrColse;
            }
            set
            {
                _OpenOrColse = value;
                OnPropertyChanged(nameof(OpenOrColse));
            }
        }

        private string _Color;
        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set
            {
                _IsRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private List<Hours> _Hours;
        public List<Hours> Hours
        {
            get { return _Hours; }
            set
            {
                _Hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        }

        private Category _SelectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private BusinessInfo _BusinessInfoProp;
        public BusinessInfo BusinessInfoProp
        {
            get
            {
                return _BusinessInfoProp;
            }
            set
            {
                _BusinessInfoProp = value;
                OnPropertyChanged(nameof(BusinessInfoProp));
            }
        }

        private BusinessInfo _currentBusinessInfo;
        public BusinessInfo CurrentBusinessInfo
        {
            get { return _currentBusinessInfo; }
            set
            {
                _currentBusinessInfo = value;
                OnPropertyChanged(nameof(CurrentBusinessInfo));
            }
        }

        private BusinessInfoWithFirstPhoto _BusinessInfoWithFirstPhoto;
        public BusinessInfoWithFirstPhoto BusinessInfoWithFirstPhoto
        {
            get { return _BusinessInfoWithFirstPhoto; }
            set
            {
                _BusinessInfoWithFirstPhoto = value;
                OnPropertyChanged(nameof(BusinessInfoWithFirstPhoto));
            }
        }

        #endregion

        #region Houres

        private TimeOnly _StartMonday;
        public TimeOnly StartMonday
        {
            get { return _StartMonday; }
            set
            {
                _StartMonday = value;
                OnPropertyChanged(nameof(StartMonday));
                AddToNewList("Monday", _StartMonday, EndMonday);
            }
        }

        private TimeOnly _EndMonday;
        public TimeOnly EndMonday
        {
            get { return _EndMonday; }
            set
            {
                _EndMonday = value;
                OnPropertyChanged(nameof(EndMonday));
                AddToNewList("Monday", _StartMonday, EndMonday);
            }
        }

        private TimeOnly _StartTuesday;
        public TimeOnly StartTuesday
        {
            get { return _StartTuesday; }
            set
            {
                _StartTuesday = value;
                OnPropertyChanged(nameof(StartTuesday));
                AddToNewList("Tuesday", _StartTuesday, EndTuesday);
            }
        }

        private TimeOnly _EndTuesday;
        public TimeOnly EndTuesday
        {
            get { return _EndTuesday; }
            set
            {
                _EndTuesday = value;
                OnPropertyChanged(nameof(EndTuesday));
                AddToNewList("Tuesday", StartTuesday, _EndTuesday);
            }
        }

        private TimeOnly _StartWednesday;
        public TimeOnly StartWednesday
        {
            get { return _StartWednesday; }
            set
            {
                _StartWednesday = value;
                OnPropertyChanged(nameof(StartWednesday));
                AddToNewList("Wednesday", _StartWednesday, EndWednesday);
            }
        }

        private TimeOnly _EndWednesday;
        public TimeOnly EndWednesday
        {
            get { return _EndWednesday; }
            set
            {
                _EndWednesday = value;
                OnPropertyChanged(nameof(EndWednesday));
                AddToNewList("Wednesday", StartWednesday, _EndWednesday);
            }
        }

        private TimeOnly _StartThursday;
        public TimeOnly StartThursday
        {
            get { return _StartThursday; }
            set
            {
                _StartThursday = value;
                OnPropertyChanged(nameof(StartThursday));
                AddToNewList("Thursday", _StartThursday, EndThursday);
            }
        }

        private TimeOnly _EndThursday;
        public TimeOnly EndThursday
        {
            get { return _EndThursday; }
            set
            {
                _EndThursday = value;
                OnPropertyChanged(nameof(EndThursday));
                AddToNewList("Thursday", StartThursday, _EndThursday);
            }
        }

        private TimeOnly _StartFriday;
        public TimeOnly StartFriday
        {
            get { return _StartFriday; }
            set
            {
                _StartFriday = value;
                OnPropertyChanged(nameof(StartFriday));
                AddToNewList("Friday", _StartFriday, EndFriday);
            }
        }

        private TimeOnly _EndFriday;
        public TimeOnly EndFriday
        {
            get { return _EndFriday; }
            set
            {
                _EndFriday = value;
                OnPropertyChanged(nameof(EndFriday));
                AddToNewList("Friday", StartFriday, _EndFriday);
            }
        }

        private TimeOnly _StartSaturday;
        public TimeOnly StartSaturday
        {
            get { return _StartSaturday; }
            set
            {
                _StartSaturday = value;
                OnPropertyChanged(nameof(StartSaturday));
                AddToNewList("Saturday", _StartSaturday, EndSaturday);
            }
        }

        private TimeOnly _EndSaturday;
        public TimeOnly EndSaturday
        {
            get { return _EndSaturday; }
            set
            {
                _EndSaturday = value;
                OnPropertyChanged(nameof(EndSaturday));
                AddToNewList("Saturday", StartSaturday, _EndSaturday);
            }
        }

        private TimeOnly _StartSunday;
        public TimeOnly StartSunday
        {
            get { return _StartSunday; }
            set
            {
                _StartSunday = value;
                OnPropertyChanged(nameof(StartSunday));
                AddToNewList("Sunday", _StartSunday, EndSunday);
            }
        }

        private TimeOnly _EndSunday;
        public TimeOnly EndSunday
        {
            get { return _EndSunday; }
            set
            {
                _EndSunday = value;
                OnPropertyChanged(nameof(EndSunday));
                AddToNewList("Sunday", StartSunday, _EndSunday);
            }
        }

        #endregion

        #region Command

        private ICommand? _submitCommand;
        public ICommand? SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new Command(
                    async (p) =>
                    {
                        await GetCachedLocation();
                        await InsertBusiness();
                        
                    },
                    (p) =>
                    {
                        return true;
                    }
                    ));
            }
        }

        private ICommand? _submitCommandGetGPS;
        public ICommand? SubmitCommandGetGPS
        {
            get
            {
                return _submitCommandGetGPS ?? (_submitCommandGetGPS = new Command(
                    async (p) =>
                    {
                        await GetCachedLocation();
                    },
                    (p) =>
                    {
                        return true;
                    }
                    ));
            }
        }


        private ICommand? _submitCommandPhoto;
        public ICommand? SubmitCommandPhoto
        {
            get
            {
                return _submitCommandPhoto ?? (_submitCommandPhoto = new Command(
                    async (p) =>
                    {
                        await PhotoStorge();
                    },
                    (p) =>
                    {
                        return true;
                    }
                    ));
            }
        }

        private ICommand? _submitCommandGetInfo;
        public ICommand? SubmitCommandGetInfo
        {
            get
            {
                return _submitCommandGetInfo ?? (_submitCommandGetInfo = new Command(
                    async (p) =>
                    {
                        await GetUserBusiness();
                    },
                    (p) =>
                    {
                        return true;
                    }
                    ));
            }
        }


        private ICommand? _SubmitCommandUpdateBusiness;
        //public ICommand? SubmitCommandUpdateBusiness
        //{
        //    get
        //    {
        //        return _SubmitCommandUpdateBusiness ?? (_SubmitCommandDelete = new Command<BusinessInfo>(
        //        async (businessInfo) =>
        //        {
        //            if (businessInfo != null)
        //            {
        //                await UpdateBusiness(businessInfo);
        //            }
        //        },
        //        (businessInfo) =>
        //        {
        //            return businessInfo != null;
        //        }
        //        ));
        //    }
        //}

        //private ICommand? _SubmitCommandDelete;
        //public ICommand? SubmitCommandDelete
        //{
        //    get
        //    {
        //        return _SubmitCommandDelete ?? (_SubmitCommandDelete = new Command<BusinessInfo>(
        //        async (businessInfo) =>
        //        {
        //            if (businessInfo != null)
        //            {
        //                await DeleteBusiness(businessInfo.Id);
        //            }
        //        },
        //        (businessInfo) =>
        //        {
        //            return businessInfo != null;
        //        }
        //    ));
        //    }
        //}

        private ICommand? _FavBusinessCommand;
        public ICommand FavBusinessCommand
        {
            get
            {
                return _FavBusinessCommand ?? (_FavBusinessCommand = new Command(
                    (p) => ShowNextBusiness(),
                    (p) => BusinessInfo != null && BusinessInfo.Count > 0 && currentIndex < BusinessInfo.Count - 1));
            }
        }

        private ICommand? _GetFavBusinessCommand;
        public ICommand GetFavBusinessCommand
        {
            get
            {
                return _GetFavBusinessCommand ?? (_GetFavBusinessCommand = new Command(
                    async (p) =>
                    {
                        await GetFavBusiness();


                    },
                    (p) =>
                    {
                        return true;
                    }
                    ));
            }
        }

        private ICommand? _LikedBusinessCommand;
        public ICommand LikedBusinessCommand
        {
            get
            {
                return _LikedBusinessCommand ?? (_LikedBusinessCommand = new Command(
                    (p) =>
                    {
                        ShowNextBusiness();
                    },
                    (p) => BusinessInfo != null && BusinessInfo.Count > 0 && currentIndex < BusinessInfo.Count - 1));
            }
        }

        private ICommand? _LikedBusinessCommandPhoto;
        public ICommand LikedBusinessCommandPhoto
        {
            get
            {
                return _LikedBusinessCommandPhoto ?? (_LikedBusinessCommandPhoto = new Command(
                    (p) => ShowNextPhoto(),
                    (p) => BusinessInfosWithFirstPhoto != null && BusinessInfosWithFirstPhoto.Count > 0 && currentIndex < BusinessInfosWithFirstPhoto.Count - 1));
            }
        }

        private ICommand? _DislikedBusinessCommand;
        public ICommand DislikedBusinessCommand
        {
            get
            {
                return _DislikedBusinessCommand ?? (_DislikedBusinessCommand = new Command(
                    (p) => ShowNextBusiness(),
                    (p) => BusinessInfo != null && BusinessInfo.Count > 0 && currentIndex < BusinessInfo.Count - 1));
            }
        }

        #endregion


        #region Methodes
        private void PopulateBusinessInfosWithFirstPhoto()
        {
            BusinessInfosWithFirstPhoto.Clear();
            foreach (var businessInfo in BusinessInfosByIdlist)
            {

                BusinessInfosWithFirstPhoto.Add(new BusinessInfoWithFirstPhoto
                {
                    BusinessInfoProp = businessInfo,
                    FirstPhotoLink = businessInfo.PhotoLinks?.FirstOrDefault()
                });
            }
        }

        private void ExecuteRefreshCommand()
        {
            IsRefreshing = true;
            BusinessInfosWithFirstPhoto.Clear();
            PopulateBusinessInfosWithFirstPhoto();
            IsRefreshing = false;
        }

        public void ShowNextBusiness()
        {

            if (currentIndex < BusinessInfo.Count - 1)
            {
                currentIndex++;
                CurrentBusinessInfo = BusinessInfo[currentIndex];
            }
        }

        public bool ShowNextPhoto()
        {

            if (currentIndex < BusinessInfosWithFirstPhoto.Count - 1)
            {
                currentIndex++;
                BusinessInfoWithFirstPhoto = BusinessInfosWithFirstPhoto[currentIndex];
                return true;
            }
            else
            {
                return false;
            }

        }

        private async void OnFrameTapped()
        {
            var bus = BusinessInfosWithFirstPhoto[currentIndex];
            UpdateBusinessView(bus);
            OpenTimeLeft();
            if (_shell != null)
            {
                await Shell.Current.GoToAsync("ViewBusinessInfo");

            }
        }

        private void OnDeleteCommand(int businessInfoId)
        {
            
            var responce = restApiUser.DeleteFav(UserId, businessInfoId);
            
        }

        private async void GoToBusiness(object parameter)
        {
            if (parameter is BusinessInfoWithFirstPhoto businessInfo)
            {
                await UpdateBusiness(businessInfo.BusinessInfoProp);
            }
        }
        private void StartTimer()
        {
            countdownTimer = new Timer(1000);
            countdownTimer.Elapsed += OnTimerElapsed;
            countdownTimer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (TimeLeft.TotalSeconds > 0)
            {
                TimeLeft = TimeLeft - TimeSpan.FromSeconds(1);
            }
            else
            {
                countdownTimer.Stop();
            }
        }

       
        private void AddToNewList(string day, TimeOnly start, TimeOnly end)
        {
            var hour = thenewlist.FirstOrDefault(h => h.Day == day);
            if (hour == null)
            {
                hour = new Hours2 { Day = day };
                thenewlist.Add(hour);
            }
            hour.Time_Open = start;
            hour.Time_Closed = end;
        }

        public async Task AddToFav()
        {
            var businessId = BusinessInfosWithFirstPhoto[currentIndex].BusinessInfoProp.Id;
            var favorite = await restApiUser.SendToFavourited(businessId, UserId);

        }

        public async Task AddToLiked()
        {
            var businessId = BusinessInfosWithFirstPhoto[currentIndex].BusinessInfoProp.Id;
            var favorite = await restApiUser.SendToLiked(businessId, UserId);

        }

        public async Task AddToDisiked()
        {
            var businessId = BusinessInfosWithFirstPhoto[currentIndex].BusinessInfoProp.Id;
            var favorite = await restApiUser.SendToDislinked(businessId, UserId);

        }
        private async void GetCategories()
        {
            var categories = await restApiBusiness.GetCategories();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        public async Task GetCachedLocation()
        {
            try
            {
                var cachedLocation = await Geolocation.GetLastKnownLocationAsync();
                if (cachedLocation != null)
                {
                        GpsLongitude = cachedLocation.Longitude;
                        GpsLatitude = cachedLocation.Latitude;   
                }
            }
            catch (Exception ex)
            {
                Message = "Unable to get location: " + ex.Message;
            }
        }

        public async Task PhotoStorge()
        {
            PickOptions options = new()
            {
                PickerTitle = "Choose a Photo",
                FileTypes = FilePickerFileType.Images
            };

            var fileResult = await FilePicker.PickAsync(options);

            if (fileResult != null)
            {
                var result = await restApiBusiness.UploadLargeFile(fileResult.FullPath, userViewModel.Id);
                Photolinklist.Add(result);
                PhotosLinkUI.Add(result);
            }
        }

        //private async void DeleteBusiness(object parameter)
        //{
        //    if (parameter is BusinessInfoWithFirstPhoto businessInfo)
        //    {
        //        if (await restApiBusiness.DeleteBusinessAsync(businessInfo.BusinessInfoProp.Id))
        //        {
        //            Message = "Business has been Deleted";

        //            var businessToRemove = BusinessInfosByIdlist.FirstOrDefault(b => b.Id == businessInfo.BusinessInfoProp.Id);
        //            if (businessToRemove != null)
        //            {
        //                BusinessInfosByIdlist.Remove(businessToRemove);
        //            }

        //        }
        //    }
        //}

        private void DeleteBusiness(object parameter)
        {
            if (parameter is BusinessInfoWithFirstPhoto businessInfo)
            {
                Task.Run(async () =>
                {
                    if (await restApiBusiness.DeleteBusinessAsync(businessInfo.BusinessInfoProp.Id))
                    {
                        Message = "Business has been Deleted";

                        var businessToRemove = BusinessInfosByIdlist.FirstOrDefault(b => b.Id == businessInfo.BusinessInfoProp.Id);
                        if (businessToRemove != null)
                        {
                            BusinessInfosByIdlist.Remove(businessToRemove);
                        }
                    }
                });
            }
        }
        private async Task GetUserBusiness()
        {
            var response = await restApiBusiness.GetBusinessInfosByIdAsync(userViewModel.Id);
            BusinessInfosByIdlist.Clear();
            foreach (var business in response)
            {
                BusinessInfosByIdlist.Add(business);
            }
            PopulateBusinessInfosWithFirstPhoto();
        }

        private async Task GetFavBusiness()
        {
            var response = await restApiBusiness.GetFavorites(UserId);
            FavLikedDislikedBusinessList.Clear();
            FavLikedDislikedBusinessPhotoList.Clear();

            foreach (var businessInfo in response)
            {
                FavLikedDislikedBusinessPhotoList.Add(new BusinessInfoWithFirstPhoto
                {
                    BusinessInfoProp = businessInfo,
                    FirstPhotoLink = businessInfo.PhotoLinks?.FirstOrDefault()
                });

            }

        }
        public async Task GetAllBusiness()
        {
            var response = await restApiBusiness.GetBusinessInfosAsync(48.803247, 9.222083, 25);
            BusinessInfosByIdlist.Clear();
            foreach (var category in response)
            {
                BusinessInfosByIdlist.Add(category);
            }
        }

        public async Task GetBusinessForSwipe()
        {
            var response = await restApiBusiness.GetBusinessInfosAsync(48.803247, 9.222083, 20);
            BusinessInfo.Clear();
            foreach (var Item in response)
            {
                BusinessInfo.Add(Item);
                BusinessInfosWithFirstPhoto.Add(new BusinessInfoWithFirstPhoto
                {
                    BusinessInfoProp = Item,
                    FirstPhotoLink = Item.PhotoLinks?.FirstOrDefault()
                });
            }
        }


        public void OpenTimeLeft()
        {
            
            DateTime now = DateTime.Now;
            string currentDay = now.DayOfWeek.ToString();
            TimeOnly currentTime = TimeOnly.FromDateTime(now);

            var hoursToday = thenewlist.FirstOrDefault(h => h.Day == currentDay);

            if (hoursToday != null)
            {
                TimeSpan timeLeft;

                if (currentTime < hoursToday.Time_Open)
                {
                    timeLeft = hoursToday.Time_Open - currentTime;
                    TimeSpan timeLeftWithoutMicroseconds = new TimeSpan(timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
                    TimeLeft = timeLeftWithoutMicroseconds;
                    OpenOrColse = "Open in";
                    Color = "Yellow";
                }
                else if (currentTime > hoursToday.Time_Closed)
                {
                    
                    var nextDay = (DayOfWeek)(((int)now.DayOfWeek + 1) % 7);
                    var hoursNextDay = thenewlist.FirstOrDefault(h => h.Day == nextDay.ToString());
                    if (hoursNextDay != null)
                    {
                        timeLeft = hoursNextDay.Time_Open.ToTimeSpan() + (TimeSpan.FromDays(1) - currentTime.ToTimeSpan());
                        TimeSpan timeLeftWithoutMicroseconds = new TimeSpan(timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
                        TimeLeft = timeLeftWithoutMicroseconds;
                        OpenOrColse = "Cloed reopen in";
                        Color = "Red";
                    }
                    else
                    {
                        timeLeft = TimeSpan.Zero;
                        TimeSpan timeLeftWithoutMicroseconds = new TimeSpan(timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
                        TimeLeft = timeLeft;
                        Color = "Red";
                    }
                }
                else
                {
                    timeLeft = hoursToday.Time_Closed - currentTime;

                    TimeSpan timeLeftWithoutMicroseconds = new TimeSpan(timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);

                    TimeLeft = timeLeftWithoutMicroseconds;
                    OpenOrColse = "Close in";
                    Color = "LightGreen";
                }

               
            }
            else
            {
                TimeLeft = TimeSpan.Zero;
                Color = "Red";
            }
        }



        #region Methode Attributes 
        private async Task InsertBusiness()
        {
            BusinessInfo businessInfo = new BusinessInfo();
            businessInfo.Id = Id;
            businessInfo.Title = Title;
            businessInfo.Category = SelectedCategory.Name;
            businessInfo.Description = Description; 
            businessInfo.PhoneNumber = PhoneNumber;
            businessInfo.Email = Email;
            businessInfo.Website = Website;
            businessInfo.PostCode = PostCode;
            businessInfo.City = City;
            businessInfo.HouseNumber = HouseNumber;
            businessInfo.Addition = Addition;
            businessInfo.Street = Street;
            businessInfo.GpsLatitude = (float)GpsLatitude;
            businessInfo.GpsLongitude= (float)GpsLongitude;
            businessInfo.UserId = userViewModel.Id;
            businessInfo.Hours = thenewlist;
            businessInfo.PhotoLinks = Photolinklist;
            await restApiBusiness.PostBusinessAsync(businessInfo);
        }

        private async Task UpdateBusiness(BusinessInfo businessInfo)
        {
            Id = businessInfo.Id;
            Title = businessInfo.Title;
            Category = businessInfo.Category;
            Description = businessInfo.Description;
            PhoneNumber = businessInfo.PhoneNumber;
            Email = businessInfo.Email;
            Website = businessInfo.Website;
            PostCode = businessInfo.PostCode;
            City = businessInfo.City;
            HouseNumber = businessInfo.HouseNumber;
            Addition = businessInfo.Addition;
            Street = businessInfo.Street;
            GpsLatitude = businessInfo.GpsLatitude;
            GpsLongitude = businessInfo.GpsLongitude;
            thenewlist = businessInfo.Hours;
            Photolinklist = businessInfo.PhotoLinks;

            if (_shell != null)
            {
                await Shell.Current.GoToAsync("BusinessPage");
            }
        }

        private void UpdateBusinessView(BusinessInfoWithFirstPhoto businessInfo)
        {
            Id = businessInfo.BusinessInfoProp.Id;
            Title = businessInfo.BusinessInfoProp.Title;
            Category = businessInfo.BusinessInfoProp.Category;
            Description = businessInfo.BusinessInfoProp.Description;
            PhoneNumber = businessInfo.BusinessInfoProp.PhoneNumber;
            Email = businessInfo.BusinessInfoProp.Email;
            Website = businessInfo.BusinessInfoProp.Website;
            PostCode = businessInfo.BusinessInfoProp.PostCode;
            City = businessInfo.BusinessInfoProp.City;
            HouseNumber = businessInfo.BusinessInfoProp.HouseNumber;
            Addition = businessInfo.BusinessInfoProp.Addition;
            Street = businessInfo.BusinessInfoProp.Street;
            GpsLatitude = businessInfo.BusinessInfoProp.GpsLatitude;
            GpsLongitude = businessInfo.BusinessInfoProp.GpsLongitude;
            var tempNewList = new List<Hours2>(businessInfo.BusinessInfoProp.Hours);
            thenewlist.Clear();

            foreach (var item in tempNewList)
            {
                thenewlist.Add(item);
            }

            foreach (var hours in tempNewList)
            {
                string day = hours.Day.Trim();

                switch (day)
                {
                    case "Monday":
                        StartMonday = hours.Time_Open;
                        EndMonday = hours.Time_Closed;
                        break;
                    case "Tuesday":
                        StartTuesday = hours.Time_Open;
                        EndTuesday = hours.Time_Closed;
                        break;
                    case "Wednesday":
                        StartWednesday = hours.Time_Open;
                        EndWednesday = hours.Time_Closed;
                        break;
                    case "Thursday":
                        StartThursday = hours.Time_Open;
                        EndThursday = hours.Time_Closed;
                        break;
                    case "Friday":
                        StartFriday = hours.Time_Open;
                        EndFriday = hours.Time_Closed;
                        break;
                    case "Saturday":
                        StartSaturday = hours.Time_Open;
                        EndSaturday = hours.Time_Closed;
                        break;
                    case "Sunday":
                        StartSunday = hours.Time_Open;
                        EndSunday = hours.Time_Closed;
                        break;
                }
            }
        }

        #endregion

        #endregion

    }

}
