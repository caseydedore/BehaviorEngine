using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorEngine.Nodes.Decorators
{
    public class Extender : ANodeDecorator
    {
        public override void Update()
        {
            Child.Update();
            Status = Child.Status;
        }
    }
}
