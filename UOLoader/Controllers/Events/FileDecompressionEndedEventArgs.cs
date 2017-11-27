using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOLoader.Controllers.Events
{
   public class FileDecompressionEndedEventArgs : EventArgs {
      public string Name { get; private set; }
      public bool BaseFile { get; }

      public FileDecompressionEndedEventArgs(string name, bool baseFile = false) {
         Name = name;
         BaseFile = baseFile;
      }
   }
}
