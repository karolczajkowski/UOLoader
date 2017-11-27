using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UOLoader.Helpers;
using UOLoader.Settings.ClientFiles;

namespace UOLoader.Files
{
   public class Files {

      public ClientFiles Info;

      private string filePath = Path.Combine(Directory.GetCurrentDirectory(), Constants.FilesConfigFileName);

      public Files() {
         if (!File.Exists(filePath)) {
            Reset();
         } else {
            Load();
         }
      }

      public void Reset() {
         Info = new ClientFiles();
         Info.LastUpdated = DateTime.Now;
         // We initialize an empty one.
         Info.Files = new List<ClientFile>();
         Save();
      }

      private void Load() {

         var contents = File.ReadAllText(filePath);
         Info = JsonConvert.DeserializeObject<ClientFiles>(contents);

         if (Info == null) {
            // TODO: Add logging.
            Reset();
         }
      }

      public void Save() {

         try {
            var contents = JsonConvert.SerializeObject(Info);
            File.WriteAllText(filePath, contents);
         } catch (Exception ex) {
            throw new Exception("Blad przy zapisywaniu informacji o plikach lokalnych. Sprawdz ustawienia antywirusowe.");
         }

      }
   }
}
