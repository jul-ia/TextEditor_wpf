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
        List<RoutedCommand> available;
        List<RoutedCommand> current;
        string filename;
        bool changed = false;

        public MainWindow()
        {
            InitializeComponent();

            available = new List<RoutedCommand>();
            available.Add(ApplicationCommands.Copy);
            available.Add(ApplicationCommands.Paste);
            available.Add(ApplicationCommands.Undo);
            available.Add(ApplicationCommands.Redo);

            current = new List<RoutedCommand>();
            current.Add(ApplicationCommands.New);

            //toolbar
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|C# file (*.cs)|*.cs|All files (*.*)|*.*" };
            if (sfd.ShowDialog() == true)
            {
                filename = sfd.FileName;
            }
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (changed)
            {
                MessageBoxResult result = MessageBox.Show("Save changes?", "Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    File.WriteAllText(filename, txtBox.Text);
                changed = false;
            }
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                filename = dlg.FileName;
                txtBox.Text = File.ReadAllText(dlg.FileName);
            }
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            File.WriteAllText(filename, txtBox.Text);
            changed = false;
        }
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|C# file (*.cs)|*.cs|All files (*.*)|*.*" };
            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, txtBox.Text);
                filename = sfd.FileName;
            }
            changed = false;
        }

        private void CommonCommandBinding_CanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

            if (current != null && current.IndexOf((RoutedCommand)e.Command) != -1)
                e.CanExecute = false;
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //todo
        }
        private void Exit_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Fx_Click(object sender, RoutedEventArgs e)
        {
            FxChange fx = new FxChange(available, current);
            fx.ShowDialog();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("Are you shure?", "Confirm", MessageBoxButton.YesNo);
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        if (changed)
        //        {
        //            result = MessageBox.Show("Save before closing?", "Confirm", MessageBoxButton.YesNo);
        //            if(result == MessageBoxResult.Yes)
        //                File.WriteAllText(filename, txtBox.Text);
        //        }                
        //    }
        //    else
        //    {
        //        e.Cancel = true;
        //    }
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Are you shure?", "Confirm", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    if (changed)
                    {
                        switch (MessageBox.Show("Save before closing?", "Confirm", MessageBoxButton.YesNo))
                        {
                            case MessageBoxResult.Yes:
                                File.WriteAllText(filename, txtBox.Text);
                                break;
                            case MessageBoxResult.No:
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
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
    }
}
