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
    /// Interaction logic for scheduleTextBox.xaml
    /// </summary>
    public partial class scheduleTextBox : UserControl, INotifyPropertyChanged
    {
        public scheduleTextBox()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string placeholder;
        private int widthSize;
        private int textboxHeightSize = 15;

        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value; OnPropertyChanged("Placeholder"); }
        }
        public int WidthSize
        {
            get { return widthSize; }
            set { widthSize = value; OnPropertyChanged("WidthSize"); }
        }
        public int TextboxHeightSize
        {
            get => textboxHeightSize;
            set { textboxHeightSize = value; OnPropertyChanged("TextboxHeightSize"); }
        }


        // event
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
