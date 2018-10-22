using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TextEditor
{
    class FileManager
    {
        public static string Filename { get; set; }
        
        public static void OpenFile(TextBox text,ref bool changed)
        {
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                Filename = dlg.FileName;
                text.Text = File.ReadAllText(dlg.FileName);
                changed = false;
            }
        }

        public static void FileSave(TextBox text, ref bool changed)
        {
            if (Filename == null || Filename.Length == 0)
            {
                var sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|C# file (*.cs)|*.cs|All files (*.*)|*.*" };
                if (sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, text.Text);
                    Filename = sfd.FileName;
                    changed = false;
                }
            }
            else
            {
                File.WriteAllText(Filename, text.Text);
                changed = false;
            }
        }

        public static void FileSaveAs(TextBox text, ref bool changed)
        {
            var sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|C# file (*.cs)|*.cs|All files (*.*)|*.*" };
            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, text.Text);
                Filename = sfd.FileName;
                changed = false;
            }
        }
    }
}
