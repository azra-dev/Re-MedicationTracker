using MedicationTracker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace MedicationTracker.MVVM.Model
{
    public class CreateScheduleModel
    {
        public long MedicationID { get; set; }
        public string? MedicationName { get; set; }
        public string? MedicationDosageValue { get; set; }
        public string? MedicationDosageForm { get; set; }
        public string? MedicationTotalAmountValue { get; set; }
        public string? MedicationExpirationDate { get; set; }
        public bool? MedicationIsPrescribed { get; set; }
        public TimeSpan? Time_1 { get; set; }
        public TimeSpan? Time_2 { get; set; }
        public TimeSpan? Time_3 { get; set; }
        public TimeSpan? Time_4 { get; set; }
        public string? MedicationPeriod { get; set; }
        public string? MedicationPeriodWeekday { get; set; }

        // PFP Image in MenuBar
        public byte[]? ProfilePicture { get; set; }

    }
}
