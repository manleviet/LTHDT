using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person() { }
        public Person(string fName, string lName)
        {
            FirstName = fName;
            LastName = lName;
        }

        public override string ToString()
        {
            return string.Format("[FirstName:{0}; LastName:{1}]",
                                 FirstName,
                                 LastName);
        }

        public override bool Equals(object obj)
        {
            return obj.ToString().Equals(this.ToString());
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
