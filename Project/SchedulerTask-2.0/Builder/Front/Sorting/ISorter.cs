using System.Collections.Generic;
using CommonTypes.Operation;

namespace Builder.Front.Sorting
{
    public interface ISorter
    {
        void Sort(List<IOperation> front);
    }
}
