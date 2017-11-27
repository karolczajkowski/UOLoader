using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOLoader.Controllers.Events
{
   public class FileDecompressionStartedEventArgs : EventArgs {

      public string Name { get; private set; }

      public FileDecompressionStartedEventArgs(string name) {
         Name = name;
      }
   }
}
