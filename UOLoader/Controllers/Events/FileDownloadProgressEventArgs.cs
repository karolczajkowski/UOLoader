using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOLoader.Controllers.Events
{
   public class FileDownloadProgressEventArgs {

      public int Percentage { get; private set; }

      public FileDownloadProgressEventArgs(int percentage) {
         Percentage = percentage;
      }
   }
}
