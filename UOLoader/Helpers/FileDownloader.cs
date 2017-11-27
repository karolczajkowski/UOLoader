using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UOLoader.Controllers.Events;

namespace UOLoader.Helpers
{
   public class FileDownloader {

      public EventHandler<FileDownloadProgressEventArgs> FileDownloadProgressChanged;

      public void DownloadFile(Uri uri, string destination)
      {
         using (var wc = new WebClient())
         {
            wc.DownloadProgressChanged += (sender, args) => { FileDownloadProgressChanged?.Invoke(this, new FileDownloadProgressEventArgs(args.ProgressPercentage));};
            var syncObj = new Object();
            lock (syncObj)
            {
               wc.DownloadFileAsync(uri, destination, syncObj);
               //This would block the thread until download completes
               Monitor.Wait(syncObj);
            }
         }

         //Do more stuff after download was complete
      }
   }
}
