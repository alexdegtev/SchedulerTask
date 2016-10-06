using Builder.Front.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Front
{
    public class FrontBuilding
    {

        public IBuilder Builder { get; set; }

        public FrontBuilding(List<Party> party, EquipmentManager equipmentManager)
        {
            Builder = new DefaultBuilder(party, equipmentManager);

        }

        public void setFrontBuildingBehaivor(IBuilder frontBuilding)
        {
            this.Builder = frontBuilding;
        }
        public void Build()
        {
            Builder.Build();
        }
    }
}
