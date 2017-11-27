using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOLoader.Controllers;
using UOLoader.Settings.RegistryChecker;

namespace UOLoader
{
   public partial class SettingsForm : Form {

      private AppController _controller;
      public SettingsForm(AppController controller) {
         _controller = controller;
         InitializeComponent();
         BindControls();
      }

      private void BindControls() {
         BrowsePictureBox.MouseEnter += ButtonEnter;
         BrowsePictureBox.MouseLeave += ButtonLeave;
         CancelPb.MouseEnter += ButtonEnter;
         CancelPb.MouseLeave += ButtonLeave;
         SavePb.MouseEnter += ButtonEnter;
         SavePb.MouseLeave += ButtonLeave;

         PathLabel.Text = _controller.Settings.Values.UltimaPath;
      }

      private void ButtonLeave(object sender, EventArgs e) {
         (sender as PictureBox).Image = UOLoaderResources.BrowseButton;
      }

      private void ButtonEnter(object sender, EventArgs e) {
         (sender as PictureBox).Image = UOLoaderResources.BrowseButton_Hovered;
      }

      private void BrowsePictureBox_Click(object sender, EventArgs e) {

         var ofd = new FolderBrowserDialog() {ShowNewFolderButton = true};
         var result = ofd.ShowDialog();

         if (result == DialogResult.Cancel) {
            return;
         }

         PathLabel.Text = ofd.SelectedPath;

      }

      private void CancelPb_Click(object sender, EventArgs e) {
         Close();
      }

      private void SavePb_Click(object sender, EventArgs e) {
         _controller.Settings.Values.UltimaPath = PathLabel.Text;
         RegistryChecker.InstallUoLoader(PathLabel.Text, _controller.Settings.Values.Version);
         if (_controller.Settings.Save()) {
            MessageBox.Show("Pomyślnie zapisano ustawienia UO Loader.", "Zapis pomyślny", MessageBoxButtons.OK,
               MessageBoxIcon.Information);
            Close();
            return;
         }

         MessageBox.Show("Nie udało się zapisać ustawien UO Loader. Sprawdź software antywirusowy.", "Zapis nieudany", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
         Close();
      }
   }
}
