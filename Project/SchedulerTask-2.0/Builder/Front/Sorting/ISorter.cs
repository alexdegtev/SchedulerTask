using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder.Front.Sorting
{
    public interface ISorter
    {
        void Sort(List<IOperation> front);
    }
}
