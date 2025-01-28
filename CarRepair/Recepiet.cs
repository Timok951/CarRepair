using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepair
{
    public class Receipt
    {
        public string CarNumber { get; set; }
        public string SparePartName { get; set; }
        public string Status { get; set; }
        public string STOName { get; set; }
        public string WorkDescription { get; set; }
        public decimal TotalPrice { get; set; }
        public string DateRequest { get; set; }
    }
}
