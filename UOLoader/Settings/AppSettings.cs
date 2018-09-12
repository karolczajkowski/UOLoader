using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UOLoader.Settings
{
   public class AppSettings {

      private ProgramSettings.ProgramSettings m_Settings;

      public ProgramSettings.ProgramSettings Values => m_Settings;

      private readonly string m_ConfigFile = Path.Combine(Directory.GetCurrentDirectory(), ".settings");

      public AppSettings() {
         LoadOrResetSettings();
      }

      public bool Save() {
         return WriteSettings();
      }

      private void LoadOrResetSettings() {

         if (!File.Exists(m_ConfigFile)) {
            SetDefaultValues();
         }
         else {
            LoadValuesFromConfig();
         }
      }

      private void SetDefaultValues() {
         
         m_Settings = new ProgramSettings.ProgramSettings() {
            AdminEmail = "support@ultima-dm.pl",
            BaseUltimaDownloadUri = "http://ultima-dm.pl/dmr.zip",
            FacebookLink = "https://www.facebook.com/Dream-Masters-Revolution-141490746505209/",
            ServerInformationUri = "http://ultima-dm.pl/server.json",
            ShardName = "Dream Masters: Revolution",
            Version = 2,
            WebsiteLink = "http://ultima-dm.pl",
            WindowTitle = $"UOLoader for Dream Masters: Revolution",
            UltimaPath = RegistryChecker.RegistryChecker.GetUoPath()
         };

         // Not using WriteSettings in order to be able to throw exceptions. May not be the cleanest way of doing things.
         var jsonContents = JsonConvert.SerializeObject(m_Settings);

         try {
            File.WriteAllText(m_ConfigFile, jsonContents);
         }
         catch (Exception ex) {
            throw ex;
         }
      }

      private void LoadValuesFromConfig() {

         var jsonContents = File.ReadAllText(m_ConfigFile);

         if (String.IsNullOrEmpty(jsonContents)) {
            SetDefaultValues();
            return;
         }

         m_Settings = JsonConvert.DeserializeObject<ProgramSettings.ProgramSettings>(jsonContents);

      }

      private bool WriteSettings() {

         var jsonContents = JsonConvert.SerializeObject(m_Settings);

         try {
            File.WriteAllText(m_ConfigFile, jsonContents);
            return true;
         } catch {
            return false;
         }
      }

   }
}
