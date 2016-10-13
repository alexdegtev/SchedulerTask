using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Front.Sorting
{
    public interface ISorter
    {
        void Sort();

        void Sort(List<IOperation> front);
    }
}
