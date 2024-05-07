using Mailjet.Client.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationTracker.Core
{
    public static class ServiceLocator
    {
        private static LoggedInUser _currentUser;

        public static LoggedInUser CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        private static CustomReminderID _customRem;

        public static CustomReminderID CustomRem
        {
            get => _customRem;
            set => _customRem = value;
        }
    }
}
