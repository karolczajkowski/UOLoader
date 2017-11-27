using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using UOLoader.Controllers;
using UOLoader.Controllers.Events;
using UOLoader.Settings.ClientFiles;
using UOLoader.Settings.ServerInformation;

namespace UOLoader.Helpers
{
   public class Patcher {

      private AppController _controller;

      public Patcher(AppController controller) {
         _controller = controller;
      }

      public async Task CheckFiles() {

         var localFiles = _controller.Files.Info;
         var serverFiles = _controller.DownloadedInformation;

         foreach (var file in serverFiles.Files) {

            var localFile = localFiles.Files.FirstOrDefault(f => f.Name == file.Name);
            // We don't even have the file - possibly an addition or a new installation of UO Loader
            if (localFile == null) {
               await ProcessFile(file);
               InsertOrUpdateLocalFile(file);
            } else if (localFile.Version < file.Version) {
               await ProcessFile(file);
               InsertOrUpdateLocalFile(file);
            }

         }
      }

      private async Task ProcessFile(ServerFile file) {

         _controller.FileDownloadStarted?.Invoke(this, EventArgs.Empty);
         await _controller.DownloadFile(new Uri(file.Uri),
            Path.Combine(_controller.Settings.Values.UltimaPath, file.Name));
         _controller.FileDownloadFinished?.Invoke(this, EventArgs.Empty);

         if (file.RequiresUnzip) {
            _controller.FileDecompressionStarted(this, new FileDecompressionStartedEventArgs(file.Name));

            using (ZipFile zip = ZipFile.Read(Path.Combine(_controller.Settings.Values.UltimaPath, file.Name))) {
               zip.ExtractAll(_controller.Settings.Values.UltimaPath,
                  ExtractExistingFileAction.OverwriteSilently);
            }

            _controller.FileDecompressionEnded?.Invoke(this, new FileDecompressionEndedEventArgs(file.Name));

         }
      }

      private void InsertOrUpdateLocalFile(ServerFile file) {

         var clientFile = _controller.Files.Info.Files.FirstOrDefault(f => f.Name == file.Name);

         if (clientFile == null) {
            _controller.Files.Info.Files.Add(new ClientFile() {
               Name = file.Name,
               Version = file.Version
            });

            _controller.Files.Info.LastUpdated = DateTime.Now;
            _controller.Files.Save();
            return;
         }

         if (clientFile.Version < file.Version) {
            clientFile.Version = file.Version;
            _controller.Files.Info.LastUpdated = DateTime.Now;
            _controller.Files.Save();
         }

      }
   }
}
