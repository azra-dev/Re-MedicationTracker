using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicationTracker.Core
{
    public class CustomReminderID : DependencyObject
    {
        public static readonly DependencyProperty CustomReminderIDProperty =
        DependencyProperty.Register(nameof(CustomRem_ID), typeof(long?), typeof(CustomReminderID), new PropertyMetadata(0L));

        public long? CustomRem_ID
        {
            get => (long?)GetValue(CustomReminderIDProperty);
            set => SetValue(CustomReminderIDProperty, value);
        }

        public CustomReminderID(long? rem_ID)
        {
            CustomRem_ID = rem_ID;
        }
    }
}
