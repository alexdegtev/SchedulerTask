using Builder.Front.Building;
using Builder.Front.Sorting;
using System.Collections.Generic;
using CommonTypes.Party;

namespace Builder.Front
{
    public class FrontBuilding
    {
        public IBuilder Builder { get; set; }
        public ISorter Sorter { get; set; }

        public FrontBuilding(List<IParty> party, ISorter sorter)
        {
            Builder = new DefaultBuilder(party, sorter);
        }

        public FrontBuilding(List<IParty> party)
        {
            Builder = new DefaultBuilder(party, new SortFront());            
        }

        public void SetFrontBuildingBehaivor(IBuilder frontBuilder)
        {
            Builder = frontBuilder;
        }

        public void Build()
        {
            Builder.Build();
        }
    }
}
