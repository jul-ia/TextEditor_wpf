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
using System.Reflection;

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для About.xaml
    /// </summary>
    public partial class About : Window
    {
        //todo

        public About()
        {
            InitializeComponent();
            Assembly assembly = Assembly.GetExecutingAssembly();
            l1.Content = assembly.GetName().Name;
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            txt.Text = String.Format("Опис: {0}\nВерсія: {1}\nCopyright: {2}\n", 
                ((AssemblyDescriptionAttribute)attributes[0]).Description, 
                assembly.GetName().Version.ToString(), 
                assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright);
        }
    }
}
