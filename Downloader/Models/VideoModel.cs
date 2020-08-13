using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Downloader.Models
{
    public class VideoModel : INotifyPropertyChanged
    {
        private bool selected;
        private string imageLocation;
        private string title;
        private string author;
        private string artist;
        private string songName;
        private bool downloaded;
        private string url;
        private decimal percent;
        private string status;

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public string ImageLocation
        {
            get { return imageLocation; }
            set
            {
                imageLocation = value;
                OnPropertyChanged("ImageLocation");
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }
        public string Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                OnPropertyChanged("Artist");
            }
        }
        public string SongName
        {
            get { return songName; }
            set
            {
                songName = value;
                OnPropertyChanged("SongName");
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public bool Downloaded
        {
            get { return downloaded; }
            set
            {
                downloaded = value;
                OnPropertyChanged("Downloaded");
            }
        }
        public decimal Percent
        {
            get { return percent; }
            set
            {
                percent = value;
                OnPropertyChanged("Percent");
            }
        }

        public string Url {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        } 


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }

    public interface IYouTubeSearch
    {

    }
    public class DownloaderViewmodel
    {
        private ObservableCollection<VideoModel> _videoList;
        public string SearchTerm;
        public DownloaderViewmodel()
        {
            _videoList = new ObservableCollection<VideoModel>();
        }
        public  bool PerformSearch()
        {
            var t = SearchTerm;
            return true;
        }
}


    public class User : INotifyPropertyChanged
    {
        private int userId;
        private string firstName;
        private string lastName;
        private string city;
        private string state;
        private string country;
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }

    class UserViewModel
    {
        private IList<User> _UsersList;

        public UserViewModel()
        {
            _UsersList = new List<User>
            {
                new User{UserId = 1,FirstName="Raj",LastName="Beniwal",City="Delhi",State="DEL",Country="INDIA"},
                new User{UserId=2,FirstName="Mark",LastName="henry",City="New York", State="NY", Country="USA"},
                new User{UserId=3,FirstName="Mahesh",LastName="Chand",City="Philadelphia", State="PHL", Country="USA"},
                new User{UserId=4,FirstName="Vikash",LastName="Nanda",City="Noida", State="UP", Country="INDIA"},
                new User{UserId=5,FirstName="Harsh",LastName="Kumar",City="Ghaziabad", State="UP", Country="INDIA"},
                new User{UserId=6,FirstName="Reetesh",LastName="Tomar",City="Mumbai", State="MP", Country="INDIA"},
                new User{UserId=7,FirstName="Deven",LastName="Verma",City="Palwal", State="HP", Country="INDIA"},
                new User{UserId=8,FirstName="Ravi",LastName="Taneja",City="Delhi", State="DEL", Country="INDIA"}
            };
        }

        public IList<User> Users
        {
            get { return _UsersList; }
            set { _UsersList = value; }
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members  

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }

            #endregion
        }
    }
}
