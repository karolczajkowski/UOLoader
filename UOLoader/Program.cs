using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOLoader.Controllers;

namespace UOLoader
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {

         var appController = new AppController();
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         EntryPoint entry = new EntryPoint(appController);
      }
   }
}
