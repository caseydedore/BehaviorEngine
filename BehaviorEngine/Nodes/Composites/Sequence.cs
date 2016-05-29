using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public class Sequence : ANodeComposite
    {
        private int index = 0,
                    indexLastActive = -1;


        public override void Update()
        {
            for (; index < Children.Count; index++)
            {
                if (indexLastActive != index)
                {
                    indexLastActive = index;
                    Children[index].Start();
                }
                Children[index].Update();
                Status = Children[index].Status;

                if (Status == NodeState.Successful) Children[index].End();
                else
                {
                    if (Status == NodeState.Failure || Status == NodeState.Error)
                    {
                        End();
                    }
                    break;
                }
            }
        }

        public override void Start()
        {
            base.Start();
            index = 0;
            indexLastActive = -1;
        }

        public override void End()
        {
            base.End();
            //if this is called through a parent, then all children NEED to be ended if they can
            foreach (var c in Children) c.End();
            index = 0;
            indexLastActive = -1;
        }
    }
}
