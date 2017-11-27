using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UOLoader.Settings.News {
    [Serializable]
    public class NewsElement {

      [JsonConverter(typeof(StringEnumConverter))]
      public ChangeType Type { get; set; }
      public string Scope { get; set; }
      public string ChangeText { get; set; }
      public string Author { get; set; }

    }
}
