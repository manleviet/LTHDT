using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregation
{
    class Engine
    {
        string name;
        public string Name
        {
            get
            {
                if (name == null)
                    return "";
                else
                    return name;
            }
            set { name = value; }
        }

        public Engine(string name)
        {
            this.name = name;
        }
    }
}
