using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.IO;
using Builder.Equipment;
using Builder.Front;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            BuilderScheduler builder = new BuilderScheduler(
                "D:/SchedulerTask-2.0/SchedulerTask/Project/SchedulerTask-2.0/TestData/TestDataBuilder/test1/",
                "D:/SchedulerTask-2.0/SchedulerTask/Project/SchedulerTask-2.0/TestData/TestDataBuilder/test1/");
            builder.Run();
        }
    }
}
