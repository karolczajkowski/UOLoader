using System;
using System.Collections.Generic;
using System.Text;

namespace UOLoader.Settings.ClientFiles
{
    [Serializable]
    public class ClientFiles {

      public DateTime LastUpdated { get; set; }
      public List<ClientFile> Files { get; set; }

    }

    [Serializable]
    public class ClientFile {
       public string Name { get; set; }
       public int Version { get; set; }
    }

    
}
