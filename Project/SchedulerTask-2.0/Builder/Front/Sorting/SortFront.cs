using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            if (a.GetParty().getPriority() > b.GetParty().getPriority())
                res = 1;
            if (a.GetParty().getPriority() == b.GetParty().getPriority())
                res = 0;
            if (a.GetParty().getPriority() < b.GetParty().getPriority())
                res = -1;

            return res;
        }

        public void Sort(List<IOperation> front)
        {
            front.Sort(comparision);
        }
    }
}
