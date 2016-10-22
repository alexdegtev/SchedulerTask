using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class TreeIterator
    {
        public Party Current { get; set; }

        public TreeIterator(Party aRoot)
        {
            NodeQueue.Enqueue(aRoot);
        }

        public bool next()
        {
            if (NodeQueue == null || NodeQueue.Count == 0)
                return false;

            Current = NodeQueue.Dequeue();

            foreach (Party subPart in Current.getSubParty())
            {
                NodeQueue.Enqueue(subPart);
            }

            return true;
       
        }

        private Queue<Party> NodeQueue
        {
            get { return mNodeQueue; }
        }
        private readonly Queue<Party> mNodeQueue = new Queue<Party>();

    }
}
