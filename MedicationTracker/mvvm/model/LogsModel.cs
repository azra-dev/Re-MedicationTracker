using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace MedicationTracker.MVVM.Model
{
    public class LogsModel
    {
        public long? User_ID { get; set; }

        public long? Med_ID { get; set; }

        public string? MedLastTaken { get; set; }

        public string? MedCumulativeIntake { get; set; }


    }
}
