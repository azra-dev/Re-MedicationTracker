using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicationTracker.Core
{
    public static class ReminderIDAttachedProperty
    {
        public static readonly DependencyProperty ReminderIDProperty =
          DependencyProperty.RegisterAttached(
            "ReminderID",
            typeof(long),
            typeof(ReminderIDAttachedProperty),
            new FrameworkPropertyMetadata(0L, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetReminderID(DependencyObject element, long value)
        {
            element.SetValue(ReminderIDProperty, value);
        }

        public static long GetReminderID(DependencyObject element)
        {
            return (long)element.GetValue(ReminderIDProperty);
        }
    }
}
