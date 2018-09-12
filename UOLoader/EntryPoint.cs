using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UOLoader.Controllers;
using UOLoader.Settings.RegistryChecker;

namespace UOLoader
{
   public class EntryPoint {

      public EntryPoint(AppController controller) {

         if (!RegistryChecker.IsUoLoaderInstalled()) {
            var form = new InstallationForm(controller);
            form.ShowDialog();
            // Fix to a bug where Setting's UO path was incorrect
            controller.Settings.Values.UltimaPath = RegistryChecker.GetUoPath();
            controller.Settings.Save();
         }

         

         var mainForm = new MainForm(controller);
         mainForm.ShowDialog();


      }
   }
}
