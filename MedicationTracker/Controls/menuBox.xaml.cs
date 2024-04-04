using MedicationTracker.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MedicationTracker.Controls
{
    /// <summary>
    /// Interaction logic for menuBox.xaml
    /// </summary>
    public partial class menuBox : UserControl, INotifyPropertyChanged
    {
        public menuBox()
        {
            DataContext = this;
            InitializeComponent();
        }

        // data binding
        public string menuText;
        public string MenuText { get { return menuText; } set { menuText = value; OnPropertyChanged("MenuText"); } }

        public string menuIcon;
        public string MenuIcon
        {
            get { return menuIcon; }
            set { menuIcon = value; OnPropertyChanged("MenuIcon"); }
        }


        // event
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
