using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace MedicationTracker.MVVM.Model
{
    public class RegisterModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        //public byte[]? ProfilePicture { get; set; }
        public string? ProfilePicturePath { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? BirthDate { get; set; }
    }
}
