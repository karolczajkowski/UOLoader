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
         }

         var mainForm = new MainForm(controller);
         mainForm.ShowDialog();

         // Below we will show our main form.
         //var mainForm = 

      }
   }
}
