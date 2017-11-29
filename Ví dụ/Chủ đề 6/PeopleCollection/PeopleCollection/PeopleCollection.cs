using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCollection
{
    class PeopleCollection
    {
        List<Person> arrPeople = new List<Person>();

        public Person this[int index]
        {
            get
            {
                if (index >= 0 && index < Count)
                    return arrPeople[index];
                return null;
            }
            set
            {
                //if (index >= 0 && index < Count)
                    arrPeople.Insert(index, value);
            }
        }

        public int Count
        {
            get { return arrPeople.Count; }
        }
    }
}
