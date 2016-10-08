using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Front.Building
{
    /// <summary>
    /// Класс постороение фронта
    /// </summary>
    public class DefaultBuilder : IBuilder
    {
        private List<Party> party;
        private List<Operation> operations;
        private EquipmentManager equipmentManager;
        public DefaultBuilder(List<Party> party, EquipmentManager equipmentManager)
        {
    }
        public void Build()
        {
            
        }
    }
}
