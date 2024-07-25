using FindUsHere.General.Interfaces;
using FindUsHere.Model.Models;
using FindUsHere.Model.RestApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FindUsHere.ViewModel
{
    public class BusinessViewModel : ViewModelBase
    {
        private RestApiBusiness restApiBusiness;
       

        public BusinessViewModel()
        {
            BusinessInfo = new ObservableCollection<IBusinessInfo>();
            restApiBusiness = new RestApiBusiness();

            InitializeBusinessInfoAsync();
         

        }

        public ObservableCollection<IBusinessInfo> BusinessInfo { get; set; } 

        private async void InitializeBusinessInfoAsync()
        {
            var businessInfos = await restApiBusiness.GetBusinessInfosAsync();

            foreach (var businessInfo in businessInfos)
            {
                BusinessInfo.Add(businessInfo);

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
            OnPrpertyChanged(nameof(Title));
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
            OnPrpertyChanged(nameof(Description));}
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
                OnPrpertyChanged(nameof(PhoneNumber));
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
                OnPrpertyChanged(nameof(Email));
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
                OnPrpertyChanged(nameof(Website));
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
                OnPrpertyChanged(nameof(PostCode));
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
                OnPrpertyChanged(nameof(City));
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
                OnPrpertyChanged(nameof(Street));
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
                OnPrpertyChanged(nameof(HouseNumber));
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
                OnPrpertyChanged(nameof(Addition));
            }
        }
        private float _GpsLongitude;
        public float GpsLongitude
        {
            get
            {
                return _GpsLongitude;
            }
            set
            {
                _GpsLongitude = value;
                OnPrpertyChanged(nameof(GpsLongitude));
            }
        }
        private float _GpsLatitude;
        public float GpsLatitude
        {
            get
            {
                return _GpsLatitude;
            }
            set
            {
                _GpsLatitude = value;
                OnPrpertyChanged(nameof(GpsLatitude));
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
                        await InsertBusiness();
                    },
                    (p) =>
                    {

                        return true;
                    }


                    ));

            }


        }
        private async Task InsertBusiness()
        {
            IBusinessInfo businessInfo = new BusinessInfo();
            businessInfo.Id = Id;
            businessInfo.Title = Title;
            businessInfo.Description = Description; 
            businessInfo.PhoneNumber = PhoneNumber;
            businessInfo.Email = Email;
            businessInfo.Website = Website;
            businessInfo.PostCode = PostCode;
            businessInfo.City = City;
            businessInfo.HouseNumber = HouseNumber;
            businessInfo.Addition = Addition;
            businessInfo.Street = Street;
            businessInfo.GpsLatitude = GpsLatitude;
            businessInfo.GpsLongitude= GpsLongitude;
            await restApiBusiness.PostBusinessAsync(businessInfo);


        }
    }
}
