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

    public partial class medTextBox : UserControl
    {

        // DependencyProperty for SQL connection
        public static readonly DependencyProperty TextInputtedProperty =
        DependencyProperty.Register(
            "TextInputted",
            typeof(string),
            typeof(medTextBox),
            new FrameworkPropertyMetadata(null));

        public string TextInputted
        {
            get { return (string)GetValue(TextInputtedProperty); }
            set { SetValue(TextInputtedProperty, value); }
        }

        public static readonly DependencyProperty TextboxPlaceholderProperty =
        DependencyProperty.Register(
            "TextboxPlaceholder",
            typeof(string),
            typeof(medTextBox),
            new FrameworkPropertyMetadata(null));

        public string TextboxPlaceholder
        {
            get { return (string)GetValue(TextboxPlaceholderProperty); }
            set { SetValue(TextboxPlaceholderProperty, value); }
        }


        public medTextBox()
        {
            InitializeComponent();
            RootUserControl.DataContext = this;
        }

        /*

        public string textboxPlaceholder;
        public string TextboxPlaceholder 
        { 
            get { return textboxPlaceholder; } 
            set 
            { 
                textboxPlaceholder = value;
                textPlaceholder.Text = textboxPlaceholder;
            } 
        }

        */

        // functions
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            textInput.Clear();
            textInput.Focus();
        }

        private void textInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textInput.Text))
            {
                textPlaceholder.Visibility = Visibility.Hidden;
            }
            else 
            { 
                textPlaceholder.Visibility = Visibility.Visible; 
            }
        }
    }
}
