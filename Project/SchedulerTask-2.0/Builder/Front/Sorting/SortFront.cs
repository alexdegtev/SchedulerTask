using System.Collections.Generic;
using CommonTypes.Operation;

namespace Builder.Front.Sorting
{
    /// <summary>
    /// Класс сортировки фронта
    /// </summary>
    public class SortFront : ISorter
    {
        private static int Comparision(IOperation a, IOperation b)
        {
            int res = 0;
            if (a.GetParty().GetPriority() > b.GetParty().GetPriority())
                res = 1;
            if (a.GetParty().GetPriority() == b.GetParty().GetPriority())
                res = 0;
            if (a.GetParty().GetPriority() < b.GetParty().GetPriority())
                res = -1;

            return res;
        }

        public void Sort(List<IOperation> front)
        {
            front.Sort(Comparision);
        }
    }
}
