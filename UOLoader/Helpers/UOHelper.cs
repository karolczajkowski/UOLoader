using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOLoader.Controllers;

namespace UOLoader.Helpers
{
   public class UOHelper {

      public static bool IsUoInstalled(AppController controller) {

         if (File.Exists(Path.Combine(controller.Settings.Values.UltimaPath, "dmr.file"))) {
            return true;
         }

         return false;
      }
      

   }

}
