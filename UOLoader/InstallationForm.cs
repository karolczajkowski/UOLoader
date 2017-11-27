using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOLoader.Controllers;

namespace UOLoader
{
   public partial class InstallationForm : Form {

      private AppController controller;

      public InstallationForm(AppController controller) {
         this.controller = controller;
         InitializeComponent();
         BindControls();
      }

      public void BindControls() {
         InstallationLabel.Text = String.Format(UOLoaderResources.UOLoader_InstallationText,
            controller.Settings.Values.ShardName);
         // Set the gump text
         Text = controller.Settings.Values.WindowTitle;
         InstructionsLabel.Text = String.Format(UOLoaderResources.UOLoader_InstallationInformation,
            controller.Settings.Values.ShardName);

         ShardLabel.Text = controller.Settings.Values.ShardName;

         InstallationPathLabel.Text = String.Format(UOLoaderResources.UOLoader_UOPath,
            controller.Settings.Values.ShardName);

         pictureBox2.Click += UltimaBrowseHandler;


      }

      private void UltimaBrowseHandler(object sender, EventArgs eventArgs) {
         var ofd = new FolderBrowserDialog() {
            ShowNewFolderButton = true,
            RootFolder = Environment.SpecialFolder.Desktop
         };

         var result = ofd.ShowDialog();

         if (result != DialogResult.Cancel) {
            textBox1.Text = ofd.SelectedPath;
            ContinueButton.Visible = true;
            InstallLabel.Visible = true;
         }

      }

      // hovering over the browse picture
      private void pictureBox2_MouseEnter(object sender, EventArgs e) {
         pictureBox2.Image = UOLoaderResources.BrowseButton_Hovered;
      }

      private void pictureBox2_MouseLeave(object sender, EventArgs e) {
         pictureBox2.Image = UOLoaderResources.BrowseButton;
      }

      private void ContinueButton_MouseEnter(object sender, EventArgs e) {
         ContinueButton.Image = UOLoaderResources.ContinueButton_Hover;
      }

      private void ContinueButton_MouseLeave(object sender, EventArgs e) {
         ContinueButton.Image = UOLoaderResources.ContinueButton;
      }

      private void ContinueButton_Click(object sender, EventArgs e) {

         // We call our controller's Install function here.

         try {
            controller.InstallUO(textBox1.Text, controller.Settings.Values.Version);
            MessageBox.Show("Succes");
         } catch (Exception ex) {
            MessageBox.Show(ex.Message);
         }

      }
   }
}
