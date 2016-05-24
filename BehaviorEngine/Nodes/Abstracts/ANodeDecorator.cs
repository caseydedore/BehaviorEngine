using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public abstract class ANodeDecorator : ANode
    {
        public INode Child { get; set; }


        public override void Start()
        {
            base.Start();
            Child.Start();
        }

        public override void End()
        {
            base.End();
            Child.End();
        }
    }
}
