using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicationTracker.Core
{
    public class LoggedInUser : DependencyObject
    {
        public static readonly DependencyProperty UserIDProperty =
        DependencyProperty.Register(nameof(UserID), typeof(long), typeof(LoggedInUser), new PropertyMetadata(0L));

        public long UserID
        {
            get => (long)GetValue(UserIDProperty);
            set => SetValue(UserIDProperty, value);
        }

        public LoggedInUser(long userId)
        {
            UserID = userId;
        }
    }
}
