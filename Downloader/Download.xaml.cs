using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Downloader.Models;
using Downloader.ViewModels;

namespace Downloader
{
    /// <summary>
    /// Interaction logic for Download.xaml
    /// </summary>
    public partial class Download : Window
    {
      
        public Download()
        {
            InitializeComponent();
          //  DataContext = new YoutubeViewModel();
        }

      
    }
}
