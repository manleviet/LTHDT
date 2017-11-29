using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    class Garage : IEnumerable
    {
        public Car Car1 { get; set; }
        public Car Car2 { get; set; }
        public Car Car3 { get; set; }

        public IEnumerator GetEnumerator()
        {
            yield return Car1;
            yield return Car2;
            yield return Car3;
        }
    }
}
