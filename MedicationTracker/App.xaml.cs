using Mailjet.Client.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MedicationTracker.Core;

namespace MedicationTracker
{
    public partial class App : Application
    {
        // Dependency property to access the logged-in user's ID across all MVVM components

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
