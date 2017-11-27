using System;
using System.Collections.Generic;
using System.Text;

namespace UOLoader.Settings.News {
    public enum ChangeType : Int32 {
      Addition = 0,
      Modification = 1,
      Deletion = 2,
      Lore = 3,
      Website = 4,
      Undefined = 5
    }
}
