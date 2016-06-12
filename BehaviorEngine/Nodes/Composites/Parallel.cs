
using System.Collections;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        private int index = 0;


        public override void Update()
        {
            if (Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            for (index = 0; index < Children.Count; index++)
            {
                if (Children[index].Status != NodeState.Active)
                {
                    Children[index].Start();
                }

                Children[index].Update();
                Status = Children[index].Status;

                if (Status == NodeState.Failure)
                {
                    Children[index].End();
                    return;
                }
                else if (Status == NodeState.Successful)
                {
                    Children[index].End();
                    return;
                }
            }
        }
    }
}
