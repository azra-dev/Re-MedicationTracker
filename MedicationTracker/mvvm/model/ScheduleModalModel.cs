using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace MedicationTracker.MVVM.Model
{
    public class ScheduleModalModel
    {
        public class MedicationInfo
        {
            public string? MedicationName { get; set; }
            public double? MedicationDosageValue { get; set; }
            public string? MedicationDosageUnit { get; set; }
            public string? MedicationDosageForm { get; set; }
            public double? MedicationTotalAmount { get; set; }
            public string? MedicationTotalAmountUnit { get; set; }
            public string? MedicationExpirationDate { get; set; }
            public bool? MedicationIsPrescribed { get; set; }
        }

        public class MedicationScheduleInfo
        {
            public long? MedicationID { get; set; }
            public string? Time_1 { get; set; }
            public string? Time_2 { get; set; }
            public string? Time_3 { get; set; }
            public string? Time_4 { get; set; }
            public string? MedicationPeriod { get; set; }
            public string? MedicationPeriodDate { get; set; }
            public string? MedicationPeriodWeekday { get; set; }


        }

        public class MedicationReminderInfo
        {
            public long? MedicationScheduleID { get; set; }
            public string? MedicationReminderTitle { get; set;}
            public string? MedicationReminderMessage { get; set; }
        }

        public class MedicationPrescription
        {
            // Implement this Godwyn
        }

        public class MedicationPrescriptionDoctor
        {
            // Implement this Godwyn
        }

    }
}
