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


namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<RoutedCommand> all;
        List<RoutedCommand> current;
        bool changed = false;
 
        public MainWindow()
        {
            InitializeComponent();

            all = new List<RoutedCommand>();
            all.Add(ApplicationCommands.Open);
            all.Add(ApplicationCommands.Save);
            all.Add(ApplicationCommands.SaveAs);

            current = new List<RoutedCommand>();
            current.Add(ApplicationCommands.Copy);
            current.Add(ApplicationCommands.Paste);
            current.Add(ApplicationCommands.Undo);
            current.Add(ApplicationCommands.Redo);
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (changed)
                SaveCheck();

            FileManager.Filename = string.Empty;
            txtBox.Text = "";
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (changed)
            {
                SaveCheck();
            }
            FileManager.OpenFile(txtBox, ref changed);
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FileManager.FileSave(txtBox, ref changed);
        }
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FileManager.FileSaveAs(txtBox, ref changed);
        }

        private void CommonCommandBinding_CanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CurrentCommands_CanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            if (current != null && current.IndexOf((RoutedCommand)e.Command) != -1)
                e.CanExecute = true;
            else
                e.CanExecute = false;

        }

        private void Exit_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Fx_Click(object sender, RoutedEventArgs e)
        {
            FxChange fx = new FxChange(all,current);
            fx.Owner = this;
            fx.ShowDialog();

            current.Clear();
            foreach (RoutedCommand r in fx.lb2.Items)
                current.Add(r);

            all.Clear();
            foreach (RoutedCommand r in fx.lb1.Items)
                all.Add(r);

            toolbarEdit();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    if (changed)
                        SaveCheck();
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
                    FileManager.FileSave(txtBox, ref changed);
                    break;
                case MessageBoxResult.No:
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
            //todo
            MessageBox.Show("HELP!!!");
        }

        private void toolbarEdit()
        {
            for (int i = 1; i < toolbar.Items.Count; i++)
            {
                if (toolbar.Items[i] is Button && ((Button)toolbar.Items[i]).Command != null)
                    if (!current.Contains(((Button)toolbar.Items[i]).Command))
                        ((Button)toolbar.Items[i]).Visibility = Visibility.Hidden;
            }       
        }

    }
}
