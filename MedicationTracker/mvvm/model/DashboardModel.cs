using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace MedicationTracker.MVVM.Model
{
    public class DashboardModel
    {
        public class JoinedMedicationSchedule
        {
            public string MedicationName { get; set; }
            public string MedicationDosageValue { get; set; }
            public string MedicationDosageForm { get; set; }
            public TimeSpan Time_1 { get; set; }
            public TimeSpan? Time_2 { get; set; }
            public TimeSpan? Time_3 { get; set; }
            public TimeSpan? Time_4 { get; set; }
            public string MedicationPeriod { get; set; }
            public string? MedicationPeriodDate { get; set; }
            public string? MedicationPeriodWeekday { get; set; }

        }

        public class MedicationReminder
        {
            public string MedicationReminderTitle { get; set; }
            public string MedicationReminderMessage { get; set; }
        }
    }
}
