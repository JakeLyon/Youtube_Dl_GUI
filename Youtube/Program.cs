using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NYoutubeDL;
using NYoutubeDL.Helpers;
using NYoutubeDL.Options;
using YouTubeSearch;

namespace Youtube
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        


            string querystring = "travis";
            int querypages = 1;

            VideoSearch videos = new VideoSearch();
            var items = await videos.GetVideos(querystring, querypages);

            foreach (var item in items)
            {
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("Description: " + item.getDescription());
                Console.WriteLine("Duration: " + item.getDuration());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("ViewCount: " + item.getViewCount());
                Console.WriteLine("");
                var t = item;
                await GetAudio(t.getUrl(), t.getTitle() + ".mp4");
            }

        


      Console.WriteLine("Complete");
       //     >> Download <<
       //        string link = "https://www.youtube.com/watch?v=daKz_b7LrsE";
       // IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link, false);
       //     DownloadVideo(videoInfos);

        }

       static async Task GetAudio(string location, string fileName)
        
        {
            var yt = new YoutubeDL();
            yt.Options.FilesystemOptions.Output = $@"e:\test\{fileName}";
            yt.Options.PostProcessingOptions.ExtractAudio = true;
            yt.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.mp3;
            yt.VideoUrl = location;
            yt.Options.GeneralOptions.Update = true;
            yt.YoutubeDlPath = $@"e:\test\youtube-dl.exe";


            yt.StandardOutputEvent += (sender, output) => Console.WriteLine(output);
            yt.StandardErrorEvent += (sender, errorOutput) => Console.WriteLine(errorOutput);



            //  string commandToRun = await yt.PrepareDownloadAsync();
            // Alternatively
            string commandToRun = yt.PrepareDownload();

            // Just let it run
           await  yt.DownloadAsync();

            // Wait for it
        //    yt.Download();
        }
    }
}
