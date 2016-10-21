using Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderConsole
{
    /// <summary>
    /// Клиентский код для построителя.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BuilderScheduler builder = new BuilderScheduler("D:/SchedulerTask-2.0/SchedulerTask/Project/SchedulerTask-2.0/TestData/TestDataBuilder/test1/", "D:/SchedulerTask-2.0/SchedulerTask/Project/SchedulerTask-2.0/TestData/TestDataBuilder/test1/");
            builder.Run();
        }
    }
}
