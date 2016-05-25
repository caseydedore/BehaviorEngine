using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeDecorator : ANode
    {
        public INode Child { get; set; }
    }
}
