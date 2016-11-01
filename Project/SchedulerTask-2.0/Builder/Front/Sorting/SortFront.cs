using System.Collections.Generic;

namespace Builder.Front.Sorting
{
    /// <summary>
    /// Класс сортировки фронта.
    /// </summary>
    public class SortFront : ISorter
    {
        private static int comparision(IOperation a, IOperation b)
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
            front.Sort(comparision);
        }
    }
}
