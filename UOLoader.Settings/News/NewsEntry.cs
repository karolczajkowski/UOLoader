using System;
using System.Collections.Generic;
using System.Text;

namespace UOLoader.Settings.News {
    [Serializable]
    public class NewsEntry {
      public DateTime Published { get; set; }
      public string Author { get; set; }
      public List<NewsElement> NewsElements { get; set; }

      public string Title { get; set; }
    }
}
