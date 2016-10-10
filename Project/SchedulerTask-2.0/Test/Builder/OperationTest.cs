
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Builder;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.Equipment;

namespace Test.Builder
{
    [TestClass]
    public class OperationTest
    {
        [TestMethod]
        public void TestGetID()
        {
            List<IOperation> lst=null; 
            IEquipment eq=null;
            Party part=null;
            TimeSpan duration = new TimeSpan(50,0,0);
            Operation op = new Operation(1,"Operation 1",duration, lst,eq,part);
            Assert.AreEqual(op.GetID(),1);
        }
    }
}
