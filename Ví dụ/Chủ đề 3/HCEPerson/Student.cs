using System;
using System.Collections.Generic;

namespace HCE
{
    class Student : HCEPerson
    {
        int course;
        int year;
        List<Class> classesTaken;

        public Student() { }
        public Student(int id, string name, string address, int course, int year)
            : base(id, name, address)
        {
            this.course = course;
            this.year = year;
            this.classesTaken = new List<Class>();
        }

        public string DisplayProfile()
        {
            return base.DisplayProfile()
                   + string.Format("[Course : {0}; Year : {1}; Num Of Classes Taken : {2}]",
                                   this.course, this.year, this.classesTaken.Count);
        }

        public void AddClassTaken(Class newClass)
        {
            classesTaken.Add(newClass);
        }

        public void ChangeCourse(int newCourse)
        {
            course = newCourse;
        }
    }
}
