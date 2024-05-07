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
    public partial class menuBox : UserControl, INotifyPropertyChanged
    {

        public menuBox()
        {
            InitializeComponent();
            RootUserControl.DataContext = this;
        }

        // Dependency property bindings

        public string ActiveIcon(string nonActiveIcon)
        {
            nonActiveIcon.Split(new char[] { '_', '/' });
            string activeIcon = "/Images/ACTIVE_" + nonActiveIcon[3];
            return activeIcon;
        }

        public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(menuBox));

        public ImageSource ImageSource
        {
            get { return base.GetValue(ImageSourceProperty) as ImageSource; }
            set { base.SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty MenuTextProperty =
        DependencyProperty.Register(
            "MenuText",
            typeof(string),
            typeof(menuBox),
            new FrameworkPropertyMetadata(null));

        public string MenuText
        {
            get { return (string)GetValue(MenuTextProperty); }
            set { SetValue(MenuTextProperty, value); }
        }

        // Data binding
        /*
        public string menuText;
        public string MenuText { get { return menuText; } set { menuText = value; OnPropertyChanged("MenuText"); } }

        public string menuIcon;
        public string MenuIcon
        {
            get { return menuIcon; }
            set { menuIcon = value; OnPropertyChanged("MenuIcon"); }
        }
        */

        public dynamic activeMenu = "#000000";
        public dynamic ActiveMenu
        {
            get { return activeMenu; }
            set { 
                activeMenu = value;
                if (activeMenu == "#2a43de") { activeMenu = "#2a43de"; }
                else { activeMenu = "#000000"; }
                OnPropertyChanged("ActiveMenu");

            }
        }


        public string defaultIconMode = "Visible"; // either Collapsed or Visible
        public string DefaultIconMode
        {
            get { return defaultIconMode; }
            set { 
                defaultIconMode = value;
                if (defaultIconMode == "Collapsed") negateDefaultIconMode = "Visible";
                else if (defaultIconMode == "Visible") negateDefaultIconMode = "Collapsed";
                else negateDefaultIconMode = "undefined";
                OnPropertyChanged("DefaultIconMode");
                OnPropertyChanged("NegateDefaultIconMode");
            }
        }

        public string negateDefaultIconMode = "Collapsed";
        public string NegateDefaultIconMode
        {
            get { return negateDefaultIconMode; }
            set { 
                negateDefaultIconMode = value;
                OnPropertyChanged("DefaultIconMode");
                OnPropertyChanged("NegateDefaultIconMode");
            }
        }

        // event
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RootUserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MenuText.Equals("Home")) {
                //discontnie
            }
        }
    }
}
