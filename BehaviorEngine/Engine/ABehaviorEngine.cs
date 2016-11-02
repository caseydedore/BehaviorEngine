using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorEngine
{
    public abstract class ABehaviorEngine
    {
        protected ANode Child { get; set; }
        protected List<ANode> RunningNodes { get; set; }


        public ABehaviorEngine()
        {
            RunningNodes = new List<ANode>();
        }

        public void SetRootChild(ANode node)
        {
            node.AddStartEvent(NodeStarted);
            node.AddEndEvent(NodeEnded);
            Child = node;
        }

        public void NodeStarted(ANode node)
        {
            //set up update routine for node
            RunningNodes.Add(node);
        }

        public void NodeEnded(ANode node)
        {
            //end update routine for node
            RunningNodes.Remove(node);
        }
    }
}
