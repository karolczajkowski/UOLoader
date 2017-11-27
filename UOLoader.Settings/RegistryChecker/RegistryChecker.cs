using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Win32;

namespace UOLoader.Settings.RegistryChecker
{
    public class RegistryChecker {

       public static bool IsUoLoaderInstalled() {
          return Registry.CurrentUser.OpenSubKey("UOLoader") != null;
       }

       public static bool InstallUoLoader(string uoPath, float version) {
          try {
             using (RegistryKey key = Registry.CurrentUser.CreateSubKey("UOLoader")) {
                key.SetValue("uoPath", uoPath);
                key.SetValue("programVersion", version);
             }
          } catch {
             return false;
          }
          return true;
       }

       public static string GetUoPath() {

          if (!IsUoLoaderInstalled()) {
             return String.Empty;
          }

          using (RegistryKey key = Registry.CurrentUser.OpenSubKey("UOLoader")) {
             return key.GetValue("uoPath").ToString();
          }
       }

       public static int GetVersion() {

          if (!IsUoLoaderInstalled()) {
             return -1;
          }

          using (RegistryKey key = Registry.CurrentUser.OpenSubKey("UOLoader")) {
             return Int32.Parse(key.GetValue("programVersion").ToString());
          }
       }
    }
}
