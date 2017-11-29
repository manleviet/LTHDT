using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCEPerson
{
    class Teacher : HCEPerson
    {
        public enum Position { Adjunct, Professor };

        List<Class> classesTeaching;
        Position position;

        public Teacher() { }
        public Teacher(int id, string name, string address, Position p)
            : base(id, name, address)
        {
            this.position = p;
            this.classesTeaching = new List<Class>();
        }

        public override string DisplayProfile()
        {
            return base.DisplayProfile()
                   + string.Format("[Position : {0}; Num Of Classes Teaching : {1}]",
                                   this.position.ToString(), this.classesTeaching.Count);
        }

        public void AddClass(Class newClass)
        {
            classesTeaching.Add(newClass);
        }

        public void Promote()
        {
            if (position == Position.Adjunct)
                position = Position.Professor;
        }
    }
}
