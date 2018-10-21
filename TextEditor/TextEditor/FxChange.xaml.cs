using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для FxChange.xaml
    /// </summary>
    public partial class FxChange : Window
    {
        List<RoutedCommand> all;

        public FxChange(List<RoutedCommand> c)
        {
            InitializeComponent();

            all = new List<RoutedCommand>();
            all.Add(ApplicationCommands.Open);
            all.Add(ApplicationCommands.Save);
            all.Add(ApplicationCommands.SaveAs);
            all.Add(ApplicationCommands.Copy);
            all.Add(ApplicationCommands.Paste);
            all.Add(ApplicationCommands.Undo);
            all.Add(ApplicationCommands.Redo);

            if (c.Count == 0)
                b2.IsEnabled = false;
            if (c.Count == all.Count)
                b1.IsEnabled = false;

            lb1.ItemsSource = all;

            foreach(RoutedCommand item in c)
            {
                lb2.Items.Add(item);
            }
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            if (lb2.Items.Count == 0)
                b2.IsEnabled = true;
            lb2.Items.Add(lb1.SelectedValue);
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            if (lb2.Items.Count == lb1.Items.Count)
                b1.IsEnabled = false;

            lb2.Items.Remove(lb2.SelectedItem);
        }

        private void lb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb2.Items.IndexOf(lb1.SelectedValue) < 0)
                b1.IsEnabled = true;
            else
                b1.IsEnabled = false;
        }

    }
}
