using System;
using System.Collections.Generic;
using System.Text;

namespace UOLoader.Settings.ProgramSettings
{
    [Serializable]
    public class ProgramSettings {

      public string WindowTitle { get; set; }
      public string FacebookLink { get; set; }
      public string WebsiteLink { get; set; }
      public string AdminEmail { get; set; }
      public int Version { get; set; }

      public string ServerInformationUri { get; set; }

      public string BaseUltimaDownloadUri { get; set; }

      public string ShardName { get; set; }

      public string UltimaPath { get; set; }

    }

}
