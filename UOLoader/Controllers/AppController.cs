using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using Microsoft.Win32;
using Newtonsoft.Json;
using UOLoader.Controllers.Events;
using UOLoader.Helpers;
using UOLoader.Settings;
using UOLoader.Settings.ClientFiles;
using UOLoader.Settings.News;
using UOLoader.Settings.RegistryChecker;
using UOLoader.Settings.ServerInformation;

namespace UOLoader.Controllers
{
   public class AppController {

      public AppSettings Settings;
      public Files.Files Files;
      public ServerInformation DownloadedInformation;
      public Patcher Patcher { get; }

      /* file download related events */
      public EventHandler FileDownloadStarted;
      public EventHandler<FileDownloadProgressEventArgs> FileDownloadProgressChanged;
      public EventHandler FileDownloadFinished;

      /* decompression events */
      public EventHandler<FileDecompressionStartedEventArgs> FileDecompressionStarted;
      public EventHandler<FileDecompressionEndedEventArgs> FileDecompressionEnded;


      /* connection to server for initial handshake and server information */
      public EventHandler ConnectionToServerError;
      public EventHandler ServerFileFormatError;
      public EventHandler ConnectedToServer;

      /* news */
      public EventHandler NewsDownloadError;
      public EventHandler NewsFileFormatError;
      public EventHandler<NewsLoadedEventArgs> NewsLoaded;
      public EventHandler NoNewsAvailable;

      /* updates to files */
      public EventHandler UpdatesAvailable;

      /* updates */
      public EventHandler DownloadedUpdates;

      public List<NewsEntry> News { get; private set; }
      private WebClient _wc;


      public AppController() {
         Settings = new AppSettings();
         Files = new Files.Files();
         Patcher = new Patcher(this);
         _wc = new WebClient();
         _wc.DownloadProgressChanged += (sender, args) => { FileDownloadProgressChanged?.Invoke(this, new FileDownloadProgressEventArgs(args.ProgressPercentage)); };
      }

      public bool InstallUO(string path, int version) {

         bool result = RegistryChecker.InstallUoLoader(path, version);

         if (!result) {
            throw new Exception("Unable to install UO Loader. Check system file permissions and RegEdit permissions");
         }

         return result;
      }

      public async Task DownloadFile(Uri uri, string destination) {
        await _wc.DownloadFileTaskAsync(uri, destination);
      }



      /// <summary>
      /// Signals from MainForm that the form is ready to gather information
      /// </summary>
      public async Task Ready() {
         // goqsane: This is to prevent the settings being empty bug
         if (Settings.Values.UltimaPath == String.Empty) {
            Settings.Values.UltimaPath = RegistryChecker.GetUoPath();
            Settings.Save();
         }
         await DownloadServerInformationAsync();
      }

      private async Task DownloadServerInformationAsync() {

         FileDownloadStarted?.Invoke(this, EventArgs.Empty);
         try {
            await DownloadFile(new Uri(Settings.Values.ServerInformationUri),
                  Path.Combine(Directory.GetCurrentDirectory(), Constants.ServerConfigFileName));

            // Now we try to open and deserialize the file into ServerInformation

            var fileContents =
               File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), Constants.ServerConfigFileName));

            if (String.IsNullOrEmpty(fileContents)) {
               ServerFileFormatError?.Invoke(this, EventArgs.Empty);
               return;
            }

            FileDownloadFinished?.Invoke(this, EventArgs.Empty);

            ServerInformation info = JsonConvert.DeserializeObject<ServerInformation>(fileContents);

            if (info == null) {
               ServerFileFormatError?.Invoke(this, EventArgs.Empty);
               return;
            }

            DownloadedInformation = info;

            DownloadAndParseNews(info.NewsUri);
            ParseServerFileInformation(info);


         }
         catch (Exception ex) {
            FileDownloadFinished?.Invoke(this, EventArgs.Empty);
            ConnectionToServerError?.Invoke(this, EventArgs.Empty);
         }
      }

      private async Task DownloadAndParseNews(string newsUri) {
         
         FileDownloadStarted?.Invoke(this, EventArgs.Empty);
         try {
            await DownloadFile(new Uri(newsUri),
               Path.Combine(Directory.GetCurrentDirectory(), Constants.NewsConfigFileName));

            // Now we try to open and deserialize the file into a list of NewsEntries

            var fileContents =
               File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), Constants.NewsConfigFileName));

            if (String.IsNullOrEmpty(fileContents)) {
               ServerFileFormatError?.Invoke(this, EventArgs.Empty);
               return;
            }

            FileDownloadFinished?.Invoke(this, EventArgs.Empty);

            List<NewsEntry> news = JsonConvert.DeserializeObject<List<NewsEntry>>(fileContents);

            if (news == null) {
               NewsFileFormatError?.Invoke(this, EventArgs.Empty);
               return;
            }

            if (!news.Any()) {
               NoNewsAvailable?.Invoke(this, EventArgs.Empty);
               return;
            }

            News = news;

            NewsLoaded?.Invoke(this, new NewsLoadedEventArgs(news.Count, news));

         }
         catch (Exception ex) {
            FileDownloadFinished?.Invoke(this, EventArgs.Empty);
            NewsDownloadError?.Invoke(this, EventArgs.Empty);
         }

      }

      private void ParseServerFileInformation(ServerInformation info) {

         Settings.Values.BaseUltimaDownloadUri = info.FullUoDownload;
         Settings.Save();

         foreach (var file in info.Files) {

            // fetch the same file on client side

            var localFile = Files.Info.Files.FirstOrDefault(f => f.Name == file.Name);

            if (localFile == null) {
               UpdatesAvailable?.Invoke(this, EventArgs.Empty);
               return;
            } else {
               if (localFile.Version < file.Version) {
                  UpdatesAvailable?.Invoke(this, EventArgs.Empty);
               }
            }

         }


      }

      public async Task DownloadBaseFiles() {

         FileDownloadStarted?.Invoke(this, EventArgs.Empty);
         await DownloadFile(new Uri(Settings.Values.BaseUltimaDownloadUri),
            Path.Combine(Settings.Values.UltimaPath, "uo.zip"));
         FileDownloadFinished?.Invoke(this, EventArgs.Empty);


         FileDecompressionStarted?.Invoke(this, new FileDecompressionStartedEventArgs(Constants.FileBaseName));

         using (ZipFile zip = ZipFile.Read(Path.Combine(Settings.Values.UltimaPath, "uo.zip"))) {
            zip.ExtractProgress += Zip_ExtractProgress;
            zip.ExtractAll(Settings.Values.UltimaPath, ExtractExistingFileAction.OverwriteSilently);
         }
         FileDecompressionEnded?.Invoke(this, new FileDecompressionEndedEventArgs(Constants.FileBaseName, true));

      }

      public async Task DownloadUpdates() {
         await Patcher.CheckFiles();
         DownloadedUpdates?.Invoke(this, EventArgs.Empty);

      }

      private void Zip_ExtractProgress(object sender, ExtractProgressEventArgs e) {
         
      }
   }
}
