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
        public FxChange(List<RoutedCommand> all, List<RoutedCommand> cur)
        {
            InitializeComponent();
            if (cur.Count == 0)
                b2.IsEnabled = false;
            if (cur.Count == all.Count || all.Count == 0)
                b1.IsEnabled = false;

            foreach(RoutedCommand item in cur)
            {
                lb2.Items.Add(item);
            }

            foreach (RoutedCommand item in all)
            {
                lb1.Items.Add(item);
            }
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            if (lb2.Items.Count == 0)
                b2.IsEnabled = true;
            lb2.Items.Add(lb1.SelectedValue);
            lb1.Items.Remove(lb1.SelectedValue);
            
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            if (lb2.Items.Count == lb1.Items.Count)
                b1.IsEnabled = false;
            lb1.Items.Add(lb2.SelectedItem);
            lb2.Items.Remove(lb2.SelectedItem);
        }

    }
}
