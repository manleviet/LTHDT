using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composition
{
    class Person
    {
        string name;
        Company workAt;

        public Person() : this("", null) { }
        public Person(string name) : this(name, null) {}
        public Person(string name, Company c)
        {
            this.name = name;
            this.workAt = c; // Có thể cho phép gán đối tượng ngay tại cấu tử
            // đối tượng đã được tạo ra ở bên ngoài lớp
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        // hoặc gán đối tượng mới thông qua thuộc tính
        public Company WorkAt
        {
            get { return workAt; }
            set { workAt = value; } // khi gán đối tượng thì không xóa đối tượng cũ
        }

        public void PrintStatus()
        {
            Console.WriteLine("{0}-{1}",
                              name,
                              workAt == null ? "no job" : workAt.Name);
        }
    }
}
