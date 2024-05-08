using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace MedicationTracker.MVVM.Model
{
    public class CustomReminderModel
    {
        public string? CustomReminderTitle { get; set; }

        public string? CustomReminderMessage {  get; set; }
    }
}
