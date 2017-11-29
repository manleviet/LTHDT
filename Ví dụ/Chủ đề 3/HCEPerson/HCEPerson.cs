using System;

namespace HCE
{
    class HCEPerson
    {
        protected int id;
        protected string name;
        protected string address;

        public HCEPerson() { }
        public HCEPerson(int id, string name, string address)
        {
            this.id = id;
            this.name = name;
            this.address = address;
        }

        public string DisplayProfile()
        {
            return string.Format("[Name : {0}; ID : {2}; Address : {2}]",
                                 this.name, this.id, this.address);
        }

        public void ChangeAddress(string newAddress)
        {
            this.address = newAddress;
        }
    }
}
