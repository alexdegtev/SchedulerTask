using System.Collections.Generic;

namespace Builder.Front.Sorting
{
    public interface ISorter
    {
        void Sort(List<IOperation> front);
    }
}
