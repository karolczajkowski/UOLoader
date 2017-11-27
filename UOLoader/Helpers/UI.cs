using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UOLoader.Helpers
{
   public class UI
   {

      public delegate void SetControlVisibleD(Control ctrl);
      public static void SetVisible(Control ctrl)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new SetControlVisibleD(SetVisible), ctrl);
         }
         else
         {
            ctrl.Visible = true;
         }
      }

      public delegate void SetLabelTextD(Label lbl, string text);
      public static void SetLabelText(Label lbl, string text)
      {

         if (lbl.InvokeRequired)
         {
            lbl.Invoke(new SetLabelTextD(SetLabelText), lbl, text);
         }
         else
         {
            lbl.Text = text;
         }
      }

      public delegate void SetBackColorOfControlD(Control ctrl, Color color);
      public static void SetBackColorOfControl(Control ctrl, Color color)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new SetBackColorOfControlD(SetBackColorOfControl), ctrl, color);
         }
         else
         {
            ctrl.BackColor = color;
         }
      }

      public delegate void EnableControlD(Control ctrl, bool state);
      public static void EnableControl(Control ctrl, bool state)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new EnableControlD(EnableControl), ctrl, state);
         }
         else
         {
            ctrl.Enabled = state;
         }
      }

      public delegate void SetControlInvisibleD(Control ctrl);
      public static void SetControlInvisible(Control ctrl)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new SetControlInvisibleD(SetControlInvisible), ctrl);
         }
         else
         {
            ctrl.Visible = false;
         }
      }

      public delegate void SetTextBoxTextD(TextBox tb, string text);
      public static void SetTextBoxText(TextBox tb, string text)
      {
         if (tb.InvokeRequired)
         {
            tb.Invoke(new SetTextBoxTextD(SetTextBoxText), tb, text);
         }
         else
         {
            tb.Text = text;
         }
      }

      public delegate void SetRichTextBoxTextD(RichTextBox tb, string text);
      public static void SetRichTextBoxText(RichTextBox tb, string text)
      {
         if (tb.InvokeRequired)
         {
            tb.Invoke(new SetRichTextBoxTextD(SetRichTextBoxText), tb, text);
         }
         else
         {
            tb.Text = text;
         }
      }

      public delegate void SetPictureBoxContentD(PictureBox pb, Bitmap content);
      public static void SetPictureBoxContent(PictureBox pb, Bitmap content)
      {
         if (pb.InvokeRequired)
         {
            pb.Invoke(new SetPictureBoxContentD(SetPictureBoxContent), pb, content);
         }
         else
         {
            pb.Image = content;
         }
      }

      public delegate void MoveToCarretD(RichTextBox tb);
      public static void MoveToCarret(RichTextBox tb)
      {
         if (tb.InvokeRequired)
         {
            tb.Invoke(new MoveToCarretD(MoveToCarret), tb);
         }
         else
         {
            tb.SelectionStart = tb.Text.Length;
            tb.ScrollToCaret();
            tb.ScrollToCaret();
         }
      }

      public delegate void SetControlLocationD(Control ctrl, System.Drawing.Point point);
      public static void SetControlLocation(Control ctrl, System.Drawing.Point point)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new SetControlLocationD(SetControlLocation), ctrl, point);
         }
         else
         {
            ctrl.Location = point;
         }
      }

      public delegate void SetControlSizeD(Control ctrl, Size size);
      public static void SetControlSize(Control ctrl, Size size)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new SetControlSizeD(SetControlSize), ctrl, size);
         }
         else
         {
            ctrl.Size = size;
         }
      }

      public delegate void SetComboBoxSelectedItemD(ComboBox ctrl, object item);
      public static void SetComboBoxSelectedItem(ComboBox ctrl, object item)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new SetComboBoxSelectedItemD(SetComboBoxSelectedItem), ctrl, item);
         }
         else
         {
            ctrl.SelectedItem = item;
         }
      }

      public delegate void SetComboBoxSelectedTextD(ComboBox ctrl, string text);
      public static void SetComboBoxSelectedText(ComboBox ctrl, string text)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new SetComboBoxSelectedTextD(SetComboBoxSelectedText), ctrl, text);
         }
         else
         {
            ctrl.SelectedText = text;
         }
      }

      public delegate void ForceControlRedrawD(Control ctrl);
      public static void ForceControlRedraw(Control ctrl)
      {
         if (ctrl.InvokeRequired)
         {
            ctrl.Invoke(new ForceControlRedrawD(ForceControlRedraw), ctrl);
         }
         else
         {
            ctrl.Refresh();
         }
      }

   }
}
