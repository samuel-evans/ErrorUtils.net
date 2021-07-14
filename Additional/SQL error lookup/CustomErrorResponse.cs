using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms47
{
    public class CustomErrorResponse
    {
        public string Message { get; set; }
        public int MatchingClauses { get; set; }
        public int TotalClauses { get; set; }
    }
}
