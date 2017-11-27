using System;
using System.Collections.Generic;
using System.Text;

namespace UOLoader.Settings.ServerInformation
{
    [Serializable]
    public class ServerInformation {

      public string NewsUri { get; set; }
      public DateTime UpdatedAt { get; set; }
      public List<ServerFile> Files { get; set; }

      public string FullUoDownload { get; set; }

    }

    [Serializable]
    public class ServerFile {
      public string Name { get; set; }
      public string Uri { get; set; }
      public int Version { get; set; }
      public bool RequiresUnzip { get; set; }
    }
}
