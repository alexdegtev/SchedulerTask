using Builder.Front.Building;
using Builder.Front.Sorting;
using System.Collections.Generic;

namespace Builder.Front
{
    public class FrontBuilding
    {
        public IBuilder Builder { get; set; }
        public ISorter Sorter { get; set; }

        public FrontBuilding(List<Party> party/*, EquipmentManager equipmentManager*/, ISorter sorter)
        {
            Builder = new DefaultBuilder(party, sorter);

        }

        public FrontBuilding(List<Party> party)
        {
            Builder = new DefaultBuilder(party, new SortFront());            
        }

        public void SetFrontBuildingBehaivor(IBuilder frontBuilding)
        {
            Builder = frontBuilding;
        }

        public void Build()
        {
            Builder.Build();
        }
    }
}
