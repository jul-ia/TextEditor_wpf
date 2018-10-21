using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<RoutedCommand> current;
        string filename;
        bool changed = false;
 
        public MainWindow()
        {
            InitializeComponent();

            current = new List<RoutedCommand>();
            //current.Add(ApplicationCommands.Open);

        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (changed)
                SaveCheck();

            txtBox.Text = "";
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (changed)
            {
                SaveCheck();
            }
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                filename = dlg.FileName;
                txtBox.Text = File.ReadAllText(dlg.FileName);
                changed = false;
            }
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FileSave();
        }
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|C# file (*.cs)|*.cs|All files (*.*)|*.*" };
            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, txtBox.Text);
                filename = sfd.FileName;
                changed = false;
            }
        }

        private void CommonCommandBinding_CanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CurrentCommands_CanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            if (current != null && current.IndexOf((RoutedCommand)e.Command) != -1)
            {
                e.CanExecute = true;
            }
            else
                e.CanExecute = false;

        }

        private void Exit_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Fx_Click(object sender, RoutedEventArgs e)
        {
            FxChange fx = new FxChange(current);
            fx.Owner = this;
            fx.ShowDialog();
            current.Clear();
            foreach (RoutedCommand r in fx.lb2.Items)
                current.Add(r);
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    if (changed)
                    {
                        SaveCheck();
                    }
                    break;
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void SaveCheck()
        {
            switch (MessageBox.Show("Save before closing?", "Confirm", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    FileSave();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void FileSave()
        {
            if (filename == null)
            {
                var sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|C# file (*.cs)|*.cs|All files (*.*)|*.*" };
                if (sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, txtBox.Text);
                    filename = sfd.FileName;
                    changed = false;
                }
            }
            else
            {
                File.WriteAllText(filename, txtBox.Text);
                changed = false;
            }
        }

        private void txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            changed = true;
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("HELP!!!");
        }

        private void toolbarEdit()
        {
           
        }

    }
}
