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
            public long? UserID { get; set; }
            public string? MedicationName { get; set; }
            public Decimal? MedicationDosageValue { get; set; }
            public string? MedicationDosageUnit { get; set; }
            public string? MedicationDosageForm { get; set; }
            public Decimal? MedicationTotalAmount { get; set; }
            public string? MedicationTotalAmountUnit { get; set; }
            public string? MedicationExpirationDate { get; set; }
            public string? MedicationNotes { get; set; }
            public bool? MedicationIsPrescribed { get; set; }
        }

        public class MedicationScheduleInfo
        {
            public long? MedicationID { get; set; }
            public TimeSpan? Time_1 { get; set; }
            public TimeSpan? Time_2 { get; set; }
            public TimeSpan? Time_3 { get; set; }
            public TimeSpan? Time_4 { get; set; }
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

        public class MedicationPrescriptionInfo
        {
            public long? MedicationID { get; set;}
            public string? PrescriptionStartDate {  get; set; }
            public string? PrescriptionEndDate {  get; set; }
            public string? PrescriptionInstructions { get; set; }

        }

        public class MedicationPrescriptionDoctor
        {
            public long? MedicationPrescriptionID { get; set; }
            public string? PrescriptionDoctorName {  get; set; }
            public string? PrescriptionDoctorSpecialization {  get; set; }
            public string? PrescriptionDoctorEmail {  get; set; }
            public string? PrescriptionDoctorAffiliation { get; set; }

        }

    }
}
