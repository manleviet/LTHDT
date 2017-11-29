using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregation
{
    class Car
    {
        string name;
        Engine engine;

        public Car(string name)
        {
            this.name = name;
        }

        // Xoá đối tượng trong hàm huỷ tử
        ~Car() { engine = null; }

        // Tạo đối tượng mới bên ngoài lớp rồi gán thông qua thuộc tính
        // không cần thao tác xoá đối tượng cũ
        public Engine CarEngine
        {
            get { return engine; }
            set { engine = value; }
        }

        // Tạo đối tượng mới bên trong lớp
        public void SetEngine(string nameEngine)
        {
            engine = new Engine(nameEngine);
        }

        public string GetDescription()
        {
            return string.Format("{0}-{1}", 
                                 name,
                                 engine == null ? "khong co dong co" : engine.Name);
        }
    }
}
