using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeComposite : ANode
    {
        public List<INode> Children { get; set; }

        public ANodeComposite(ABehaviorEngine master) : base(master)
        {
            Children = new List<INode>();
        }
    }
}
