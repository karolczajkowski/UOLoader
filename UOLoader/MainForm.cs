using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UOLoader.Controllers;
using UOLoader.Controllers.Events;
using UOLoader.Helpers;
using UOLoader.Settings.News;

namespace UOLoader
{
   public partial class MainForm : Form {

      private AppController controller;

      public MainForm(AppController controller) {
         this.controller = controller;
         InitializeComponent();
         BindControls();
         controller.Ready();
      }

      private void BindControls() {

         InstallUoPictureBox.MouseEnter += ButtonEnter;
         PlayPictureBox.MouseEnter += ButtonEnter;
         SettingsPictureBox.MouseEnter += ButtonEnter;

         InstallUoPictureBox.MouseLeave += ButtonLeave;
         PlayPictureBox.MouseLeave += ButtonLeave;
         SettingsPictureBox.MouseLeave += ButtonLeave;



         ShardTitleLabel.Text = controller.Settings.Values.ShardName;
         Text =
            $"UOLoader :: {controller.Settings.Values.ShardName} :: version {controller.Settings.Values.Version} :: created by Karol C.";

         shardAddressLabel.Text = controller.Settings.Values.WebsiteLink;
         shardMailLabel.Text = controller.Settings.Values.AdminEmail;

         InstallationText.Text = String.Format(InstallationText.Text, controller.Settings.Values.ShardName);
         CheckUpdatesText.Text = String.Format(CheckUpdatesText.Text, controller.Settings.Values.ShardName);

         controller.FileDownloadStarted += FileDownloadStarted;
         controller.FileDownloadProgressChanged += FileDownloadProgressChanged;
         controller.FileDownloadFinished += FileDownloadFinished;
         controller.ConnectionToServerError += ConnectionToServerError;
         controller.ServerFileFormatError += ServerFileFormatError;
         controller.NewsFileFormatError += NewsFileFormatError;
         controller.NewsDownloadError += NewsDownloadError;
         controller.NewsLoaded += NewsLoaded;
         controller.UpdatesAvailable += UpdatesAvailable;

         controller.FileDecompressionStarted += FileDecompressionStarted;
         controller.FileDecompressionEnded += FileDecompressionEnded;

         controller.DownloadedUpdates += DownloadedUpdates;

         // If DMR is installed (the base package) then we don't offer to download the files pointlessly
         if (UOHelper.IsUoInstalled(controller)) {
            UI.SetControlInvisible(InstallUoPictureBox);
            UI.SetControlInvisible(InstallationText);
         }


      }

      private void DownloadedUpdates(object sender, EventArgs eventArgs) {

         var processInfo = new ProcessStartInfo(Path.Combine(controller.Settings.Values.UltimaPath, "client_dmr.exe"));
         processInfo.WorkingDirectory = controller.Settings.Values.UltimaPath;
         Process.Start(processInfo);
      }

      private void FileDecompressionEnded(object sender, FileDecompressionEndedEventArgs fileDecompressionEndedEventArgs) {
         UI.SetLabelText(statusLabel, $"Rozpakowywanie {fileDecompressionEndedEventArgs.Name} zakończone");

         if (fileDecompressionEndedEventArgs.BaseFile) {
            UI.SetControlInvisible(InstallationText);
            UI.SetControlInvisible(InstallUoPictureBox);
         }
      }

      private void FileDecompressionStarted(object sender, FileDecompressionStartedEventArgs fileDecompressionStartedEventArgs) {
         UI.SetLabelText(statusLabel, $"Rozpakowywanie {fileDecompressionStartedEventArgs.Name} rozpoczęte");
      }

      private void FileDownloadStarted(object sender, EventArgs eventArgs) {
         ToggleDownloadProgressBar(true);
      }

      private void UpdatesAvailable(object sender, EventArgs eventArgs) {
         UI.SetVisible(UpdatePB);
         UI.SetVisible(UpdatesLabel);
         FlashWindow.Flash(this, 10);
         UI.SetLabelText(CheckUpdatesText, "Ściągnij nowe pliki i uruchom grę");
      }

      private void NewsLoaded(object sender, NewsLoadedEventArgs newsLoadedEventArgs) {

         UI.SetLabelText(NewsLabel, $"Wieści ({newsLoadedEventArgs.NumberOfNews})");
         UI.SetVisible(TitleLabel);
         UI.SetVisible(ForTitle);
         UI.SetVisible(authorLabel);

         // first we sort and only take the last news item

         List<NewsEntry> news = newsLoadedEventArgs.News;
         NewsEntry latest = news.OrderByDescending(n => n.Published).FirstOrDefault();

         UI.SetLabelText(TitleLabel, String.Format(TitleLabel.Text, latest.Title));
         UI.SetLabelText(authorLabel, String.Format(authorLabel.Text, latest.Author));

         ParseNewsAndGenerateForms(latest.NewsElements);

      }

      private void NewsDownloadError(object sender, EventArgs eventArgs) {
         UI.SetLabelText(statusLabel, $"Błąd ściągania JSON z wieściami");
      }

      private void NewsFileFormatError(object sender, EventArgs eventArgs) {
         UI.SetLabelText(statusLabel, $"Błąd odczytu pliku z newsami serwera. Błędny JSON");
      }

      private void ServerFileFormatError(object sender, EventArgs eventArgs) {
         UI.SetLabelText(statusLabel, $"Błąd odczytu pliku informacji serwera. Błędny JSON");
      }

      private void ConnectionToServerError(object sender, EventArgs eventArgs) {
         UI.SetLabelText(statusLabel, $"Błąd połączenia z serwerem. Skontaktuj się z ekipą shardu na e-mail");
      }

      private void FileDownloadFinished(object sender, EventArgs eventArgs) {
         UI.SetLabelText(statusLabel, $"Ściąganie pliku zakończone");
         ToggleDownloadProgressBar(false);
      }

      private void FileDownloadProgressChanged(object sender, FileDownloadProgressEventArgs fileDownloadProgressEventArgs) {
         UI.SetLabelText(statusLabel, $"Ściąganie: {fileDownloadProgressEventArgs.Percentage}%");
         SetDownloadProgress(fileDownloadProgressEventArgs.Percentage);
      }

      private void MainForm_Load(object sender, EventArgs e) {

      }

      private void shardAddressLabel_MouseEnter(object sender, EventArgs e) {
         shardAddressLabel.ForeColor = Color.LightBlue;
      }

      private void shardAddressLabel_MouseLeave(object sender, EventArgs e) {
         shardAddressLabel.ForeColor = Color.White;
      }

      private void ButtonEnter(object sender, EventArgs e) {
         (sender as PictureBox).Image = UOLoaderResources.BrowseButton_Hovered;
      }

      private void ButtonLeave(object sender, EventArgs e) {
         (sender as PictureBox).Image = UOLoaderResources.BrowseButton;
      }

      private void ParseNewsAndGenerateForms(List<NewsElement> elements) {

         var currentOffset = 0;
         int controlLoop = 0;

         foreach (var element in elements) {

            var pb = new PictureBox {
               Location = new Point(Constants.NewsItemBulletStartX, Constants.NewsItemBulletStartY + currentOffset),
               Name = $"pb{currentOffset}",
               Image = UOLoaderResources.magic_scroll,
               BackColor = Color.Transparent,
               SizeMode = PictureBoxSizeMode.AutoSize
            };
            //pb.Size = new Size(24, 18);
            Controls.Add(pb);
            pb.BringToFront();


            var label = new Label {
               Location =
                  new Point(Constants.NewsItemBulletStartX + Constants.XOffsetForChangeType, Constants.NewsItemBulletStartY + currentOffset),
               Name = $"lb{currentOffset}",
               Padding = Padding.Empty,
               ForeColor = GetChangeTypeColor(element.Type),
               BackColor = System.Drawing.Color.Transparent,
               Font = new Font("Consolas", 12F, FontStyle.Bold),
               Text = GetChangeTypeText(element.Type),
               AutoSize = true
            };
            Controls.Add(label);
            label.BringToFront();

            var newsLabel = new Label() {
               Location =
                  new Point(Constants.NewsItemContentStartX, Constants.NewsItemBulletStartY + 20 + currentOffset),
                  Name = $"lbText--{controlLoop}",
                  Padding = Padding.Empty,
                  ForeColor = Color.AliceBlue,
                  BackColor = System.Drawing.Color.Transparent,
                  Font = new Font("Consolas", 12F, FontStyle.Regular),
                  Text = element.ChangeText,
                  AutoSize = true,
                  MaximumSize = new Size(370, 0)
            };

            Controls.Add(newsLabel);
            newsLabel.BringToFront();



            currentOffset += Constants.LineHeight + GetLastNewsHeight();
            // If the news are longer than the window.. resize it
            if (currentOffset + Constants.NewsItemBulletStartY > Height) {

               Height += GetLastNewsHeight();
               Update();
            }
            Debug.WriteLine($"currentOffset: {currentOffset}");
            controlLoop++;

         }
      }

      private Color GetChangeTypeColor(ChangeType type) {
         switch (type) {
               case ChangeType.Addition: return Color.LightGreen;
               case ChangeType.Deletion: return Color.OrangeRed;
               case ChangeType.Lore: return Color.Brown;
               case ChangeType.Modification: return Color.Orange;
               case ChangeType.Website: return Color.Blue;
               case ChangeType.Undefined: return Color.White;
               default:
               return Color.LightBlue;
         }
      }

      private string GetChangeTypeText(ChangeType type) {
         switch (type)
         {
            case ChangeType.Addition: return "Dodano";
            case ChangeType.Deletion: return "Usunięto";
            case ChangeType.Lore: return "Historia";
            case ChangeType.Modification: return "Zmieniono";
            case ChangeType.Website: return "WWW";
            case ChangeType.Undefined: return "Ogólne";
            default:
               return "Ogólne";
         }
      }
      /// <summary>
      /// This is used for properly displaying the news items. It gets the last 
      /// </summary>
      /// <returns></returns>
      private int GetLastNewsHeight() {

         List<OrderedLabel> labels = new List<OrderedLabel>();

         var controls = Controls.OfType<Label>().Where(c => c.Name.StartsWith("lbText"));
         foreach (var control in controls) {
            labels.Add(new OrderedLabel() { Label = control, Number = Int32.Parse(control.Name.Replace("lbText--", ""))});
         }

         labels = labels.OrderByDescending(c => c.Number).ToList();

         Label lastLabel = labels.FirstOrDefault()?.Label;

         if (lastLabel == null) {
            return 0;
         }
         else {
            return lastLabel.Size.Height;
         }
      }

      private class OrderedLabel {
         public int Number { get; set; }
         public Label Label { get; set; }
      }

      private void InstallUoPictureBox_Click(object sender, EventArgs e) {
         controller.DownloadBaseFiles();
      }

      private void SetDownloadProgress(int percentage) {
         if (percentage < 10) {
            UI.SetControlInvisible(pbStep1);
         } else if (percentage >= 10 && percentage < 20) {
            UI.SetVisible(pbStep1);
         } else if (percentage >= 20 && percentage < 30) {
            UI.SetVisible(pbStep2);
         } else if (percentage >= 30 && percentage < 40) {
            UI.SetVisible(pbStep3);
         } else if (percentage >= 40 && percentage < 50) {
            UI.SetVisible(pbStep4);
         } else if (percentage >= 50 && percentage < 60) {
            UI.SetVisible(pbStep5);
         } else if (percentage >= 60 && percentage < 70) {
            UI.SetVisible(pbStep6);
         } else if (percentage >= 70 && percentage < 80) {
            UI.SetVisible(pbStep7);
         } else if (percentage >= 80 && percentage < 90) {
            UI.SetVisible(pbStep8);
         } else if (percentage >= 90 && percentage < 100) {
            UI.SetVisible(pbStep9);
         } else if (percentage >= 100) {
            UI.SetVisible(pbStep10);
         }
      }

      private void ToggleDownloadProgressBar(bool state) {


         if (state) {
            UI.SetVisible(pbBegin);
            UI.SetControlInvisible(pbStep1);
            UI.SetControlInvisible(pbStep2);
            UI.SetControlInvisible(pbStep3);
            UI.SetControlInvisible(pbStep4);
            UI.SetControlInvisible(pbStep5);
            UI.SetControlInvisible(pbStep6);
            UI.SetControlInvisible(pbStep7);
            UI.SetControlInvisible(pbStep8);
            UI.SetControlInvisible(pbStep9);
            UI.SetControlInvisible(pbStep10);
            UI.SetVisible(pbEnd);
         }
         else {
            UI.SetControlInvisible(pbBegin);
            UI.SetControlInvisible(pbStep1);
            UI.SetControlInvisible(pbStep2);
            UI.SetControlInvisible(pbStep3);
            UI.SetControlInvisible(pbStep4);
            UI.SetControlInvisible(pbStep5);
            UI.SetControlInvisible(pbStep6);
            UI.SetControlInvisible(pbStep7);
            UI.SetControlInvisible(pbStep8);
            UI.SetControlInvisible(pbStep9);
            UI.SetControlInvisible(pbStep10);
            UI.SetControlInvisible(pbEnd);
         }
      }

      private void SettingsPictureBox_Click(object sender, EventArgs e) {

         var settings = new SettingsForm(controller);
         settings.ShowDialog();


      }

      public delegate Task AsyncPlayPictureBoxHandler(object sender, EventArgs e);

      private void PlayPictureBox_Click(object sender, EventArgs e) {
         if (!UOHelper.IsUoInstalled(controller)) {
            MessageBox.Show("Najpierw zainstaluj pliki bazowe do gry.", "Błąd uruchamiania", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            return;
         }

         controller.DownloadUpdates();
      }

      private void ContinueButton_Click(object sender, EventArgs e) {
         MessageBox.Show("Funkcja nie zostala zaimplementowana");

      }
   }
}
