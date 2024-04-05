using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace MedicationTracker.Core
{
    internal class ObservableObject : INotifyPropertyChanged
    {
        public string connectionString = @"Server=192.168.1.4,1433;Database=MediTrack;User ID=tester;Password=meditrack;Integrated Security=False;Trusted_Connection=False;";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
