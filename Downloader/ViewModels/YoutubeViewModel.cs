using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Downloader.Models;
using NYoutubeDL;
using NYoutubeDL.Helpers;
using YouTubeSearch;
using Action = System.Action;

namespace Downloader.ViewModels
{
    public class SearchHandler : INotifyPropertyChanged
    {
        private ObservableCollection<VideoModel> _videos;
        public ObservableCollection<VideoModel> Videos
        {
            get { return _videos; }
            set
            {
                _videos = value;
                OnPropertyChanged("Videos");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }


public class RelayCommand : ICommand
    {
        private Action _targetMethod;
        private Func<bool> _targetCanExecute;

        public RelayCommand(Action execute)
        {
            _targetMethod = execute;
        }
        public RelayCommand(Action execute,Func<bool> canExecute)
        {
            _targetMethod = execute;
            _targetCanExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            //if (_targetCanExecute != null)
            //{

            //}

            //if (_targetMethod != null)
            //{
            //    return true;
            //}

            return true;
        }

        public void Execute(object parameter)
        {
            if (_targetMethod != null)
            {
                _targetMethod();
            }

            return;

        }

        public event EventHandler CanExecuteChanged;
    }

    public sealed class YoutubeViewModel : INotifyPropertyChanged
    {
      public  YoutubeViewModel()
        {
            SearchCommand = new RelayCommand(PerformSearch);
            AddToQueueCommand = new RelayCommand(AddToQueue);
            StartQueueCommand = new RelayCommand(ProcessQueue);
        }
      private string searchTerm;

      public string SearchTerm
      {
          get { return searchTerm; }
          set
          {
              searchTerm = value;
              OnPropertyChanged("SearchTerm");
          }
        }
      private int itemCount;

      public int ItemCount
      {
          get { return itemCount; }
          set
          {
              itemCount = value;
              OnPropertyChanged("ItemCount");
              OnPropertyChanged("Videos");
            }
      }
      
        public ObservableCollection<VideoModel> Videos { get; set; }
        public ObservableCollection<VideoModel> Queue { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand AddToQueueCommand { get; private set; }
        public RelayCommand StartQueueCommand { get; private set; }


        public async void PerformSearch()
        {
            Videos = new ObservableCollection<VideoModel>();
            string querystring = searchTerm;
            int querypages = 1;

            VideoSearch videos = new VideoSearch();
            var items = await videos.GetVideos(querystring, querypages);
            

            foreach (var i in items)
            {
                var t = new VideoModel()
                {
                    Author = i.getAuthor(),
                    Downloaded = false,
                    ImageLocation = i.getThumbnail(),
                    Selected = false,
                    Title = i.getTitle(),
                    Url = i.getUrl(),
                };


                var sp = t.Title.IndexOf('-') - 1;

                var art = "";
                var track = "";

                if (sp <= 1)
                {
                    art = t.Title.Trim();
                    track = "";
                }
                else
                {
                    art = t.Title.Substring(0, sp).Trim();
                    track = t.Title.Remove(0, sp+2).Trim();
                }


                t.Artist = art;
                t.SongName = track;

                Videos.Add(
                 t
                );
            }

            ItemCount = Videos.Count;

        }

        public void AddToQueue()
        {
            if (Queue == null)
            {
                Queue = new ObservableCollection<VideoModel>();
            }

            var items = Videos.Where(x => x.Selected).ToList();
            foreach (var item in items)
            {
                if (Queue.FirstOrDefault(x => x.Title == item.Title) == null)
                {
                    item.Status = "Queued";
                    Queue.Add(item);
                }
            }

            OnPropertyChanged("Queue");
        }

        public string consoleText;

        public string ConsoleText
        {
            get { return consoleText; }
            set
            {
                consoleText = value;
                OnPropertyChanged("ConsoleText");
            }
        }
        public bool processingQueue;

        public bool ProcessingQueue
        {
            get { return processingQueue; }
            set
            {
                processingQueue = value;
                OnPropertyChanged("ProcessingQueue");
            }
        }

        public async void ProcessQueue()
        {
            processingQueue = true;
            Parallel.ForEach(Queue, async item =>
                //foreach (var item in Queue)
            {
                if (item.Status == "Queued")
                {

                    var fileName = item.Title + ".mp4";
                    var location = item.Url;

                    var items = new List<string>();

                    var yt = new YoutubeDL();
                    yt.Options.FilesystemOptions.Output = $@"e:\test\{fileName}";
                    yt.Options.PostProcessingOptions.ExtractAudio = true;
                    yt.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.mp3;
                    yt.VideoUrl = location;
                    yt.Options.GeneralOptions.Update = true;
                    yt.YoutubeDlPath = $@"e:\test\youtube-dl.exe";
                    yt.StandardOutputEvent += (sender, output) =>
                    {

                        Console.WriteLine(output);
                        ConsoleText = output;
                        items.Add(output);

                        var pct = item.Percent;

                        if (output.Contains("[download]") && output.Contains('%'))
                        {
                            var s = output.IndexOf('%');
                            var num = output.Substring(10, s - 10);
                            var numc = num.Trim();
                            pct = Decimal.Parse(numc);
                            item.Percent = pct;
                            item.Status = "Downloading";
                        }

                        if (item.Percent == 100)
                        {
                            item.Status = "Processing";
                        }

                        if (output.StartsWith("Deleting"))
                        {
                            item.Status = "Done";
                        }


                    };
                    yt.StandardErrorEvent += (sender, errorOutput) => Console.WriteLine(errorOutput);
                    string commandToRun = yt.PrepareDownload();
                    await yt.DownloadAsync();
                }
            });

            processingQueue = false;
        }

    }
}
