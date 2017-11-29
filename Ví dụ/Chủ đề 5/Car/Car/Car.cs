using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car
{
    class Car : IComparable
    {
        public int CarID { get; set; }
        public string Name { get; set; }
        public int NumberOfWheel { get; set; }

        public Car(int id, string name, int numWheel)
        {
            CarID = id;
            Name = name;
            NumberOfWheel = numWheel;
        }

        public int CompareTo(object obj)
        {
            Car temp = obj as Car;
            if (temp != null)
            {
                return this.CarID.CompareTo(temp.CarID);
                //if (this.CarID > temp.CarID) return 1;
                //if (this.CarID < temp.CarID) return -1;
                //else return 0;
            }
            throw new ArgumentException("not a Car");
        }

        public class NameComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                Car t1 = x as Car;
                Car t2 = y as Car;
                if (t1 != null && t2 != null)
                    return String.Compare(t1.Name, t2.Name);
                else
                    throw new ArgumentException("not a Car");
            }
        }

        private class NumWheelComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                Car t1 = x as Car;
                Car t2 = y as Car;
                if (t1 != null && t2 != null)
                    return t1.NumberOfWheel.CompareTo(t2.NumberOfWheel);
                else
                    throw new ArgumentException("not a Car");
            }
        }

        public static IComparer SortByNumWheel
        {
            get { return (IComparer)new NumWheelComparer(); }
        }

        public override string ToString()
        {
            return string.Format("ID: {0}, Name: {1}, NumWheel: {2}",
                                 CarID,
                                 Name,
                                 NumberOfWheel);
        }
    }
}
