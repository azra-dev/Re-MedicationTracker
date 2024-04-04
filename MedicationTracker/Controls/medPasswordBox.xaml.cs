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
    /// Interaction logic for medPasswordBox.xaml
    /// </summary>
    public partial class medPasswordBox : UserControl, INotifyPropertyChanged
    {
        public medPasswordBox()
        {
            DataContext = this;
            InitializeComponent();
        }

        // data binding
        public string passwordPlaceholder;
        public string PasswordPlaceholder { get { return passwordPlaceholder; } set { passwordPlaceholder = value; OnPropertyChanged("PasswordPlaceholder"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // functions
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            textInput.Clear();
            textInput.Focus();
        }

        private void textInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textInput.Password)) passPlaceholder.Visibility = Visibility.Hidden;
            else passPlaceholder.Visibility = Visibility.Visible;
        }
    }
}
