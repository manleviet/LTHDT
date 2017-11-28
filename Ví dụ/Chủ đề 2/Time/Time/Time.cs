using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    class Time
    {
        int hours;
        int minutes;

        public Time(int hours, int minutes)
        {
            this.hours = hours;
            this.minutes = minutes;
        }

        public void Add(Time t)
        {
            int temp = this.minutes + t.minutes;

            if (temp >= 60)
            {
                this.minutes = temp - 60;
                this.hours++;
            }
            temp = this.hours + t.hours;

            if (temp >= 12)
                this.hours = temp - 12;
            else
                this.hours = temp;
        }

        public void PrintTime()
        {
            Console.WriteLine("{0}:{1}", this.hours, this.minutes);
        }
    }
}
