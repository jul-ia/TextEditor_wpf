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
        List<RoutedCommand> commandsL;
        List<RoutedCommand> commandsR;


        public FxChange(List<RoutedCommand> l, List<RoutedCommand> r)
        {
            InitializeComponent();
            commandsL = l;
            commandsR = r;

            if (commandsL.Count == 0)
                b2.IsEnabled = false;
            if (commandsR.Count == 0)
                b1.IsEnabled = false;
        }
    }
}
