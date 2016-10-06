using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Front.Building
{
    public class DefaultBuilder : IBuilder
    {
        private List<Party> party;
        private List<IOperation> operations;
        private EquipmentManager equipmentManager;
        public DefaultBuilder(List<Party> party, EquipmentManager equipmentManager)
        {
    }
        public void Build()
        {
            
        }
    }
}
