using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composition
{
    class TaxRegistration
    {
        public string ID { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public TaxRegistration(string id, int day, int month, int year)
        {
            this.ID = id;
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }
    }
}
