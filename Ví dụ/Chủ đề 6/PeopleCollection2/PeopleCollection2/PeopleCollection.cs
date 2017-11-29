using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCollection2
{
    class PeopleCollection
    {
        Dictionary<string, Person> dicPeople = new Dictionary<string, Person>();

        public Person this[string name]
        {
            get
            {
                if (dicPeople.ContainsKey(name))
                    return dicPeople[name];
                return null;
            }
            set
            {
                dicPeople[name] = value;
            }
        }

        public Person this[int index]
        {
            get
            {
                if (index >= 0 && index < Count)
                    return (dicPeople.Values.ToList<Person>())[index];
                return null;
            }
        }

        public int Count
        {
            get { return dicPeople.Count; }
        }
    }
}
