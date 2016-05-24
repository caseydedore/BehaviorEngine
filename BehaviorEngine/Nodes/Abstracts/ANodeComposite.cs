using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeComposite : ANode
    {
        public List<INode> Children { get; set; }

        public ANodeComposite()
        {
            Children = new List<INode>();
        }
    }
}
