using System.Collections.Generic;

namespace CommonTypes.Party
{
    public class TreePartyIterator
    {
        private readonly Queue<IParty> nodeQueue = new Queue<IParty>();

        public IParty Current { get; set; }

        public TreePartyIterator(IParty aRoot)
        {
            nodeQueue.Enqueue(aRoot);
        }

        public bool Next()
        {
            if (nodeQueue == null || nodeQueue.Count == 0)
                return false;

            Current = nodeQueue.Dequeue();

            foreach (Party subPart in Current.GetSubParty())
            {
                nodeQueue.Enqueue(subPart);
            }

            return true;
        }
    }
}
