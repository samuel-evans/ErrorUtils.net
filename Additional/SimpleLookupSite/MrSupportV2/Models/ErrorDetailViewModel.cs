using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrSupportV2.Models
{
    public class ErrorDetailViewModel
    {
        public string ErrorCode { get; set; }
        public string User { get; set; }
        public DateTime Time  { get; set; }
        public string Error { get; set; }
        public string StackTrace { get; set; }
    }
}
