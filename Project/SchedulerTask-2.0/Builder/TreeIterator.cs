using System.Collections.Generic;

namespace Builder
{
    public class TreeIterator
    {
        private readonly Queue<Party> nodeQueue = new Queue<Party>();

        public Party Current { get; set; }

        public TreeIterator(Party aRoot)
        {
            NodeQueue.Enqueue(aRoot);
        }

        public bool Next()
        {
            if (NodeQueue == null || NodeQueue.Count == 0)
                return false;

            Current = NodeQueue.Dequeue();

            foreach (Party subPart in Current.GetSubParty())
            {
                NodeQueue.Enqueue(subPart);
            }

            return true;
       
        }

        private Queue<Party> NodeQueue
        {
            get { return nodeQueue; }
        }
    }
}
