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
        public string? MedicationName { get; set; }

        public string? MedLastTaken { get; set; }

        public decimal? MedCumulativeIntake { get; set; }

    }
}
