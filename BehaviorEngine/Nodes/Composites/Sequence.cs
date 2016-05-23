using System.Collections.Generic;
using System;

namespace BehaviorEngine
{
    public class Sequence : ANodeComposite
    {
        private int index = 0,
                    indexLastActive = -1;
        private NodeState status = NodeState.Successful;


        public Sequence(ABehaviorEngine master) : base(master) { }

        public override NodeState Update()
        {
            for (; index < Children.Count; index++)
            {
                if (indexLastActive != index)
                {
                    indexLastActive = index;
                    Children[index].Start();
                }
                status = Children[index].Update();

                if (status == NodeState.Successful) Children[index].End();
                else
                {
                    if (status == NodeState.Failure || status == NodeState.Error)
                    {
                        End();
                    }
                    break;
                }
            }

            return status;
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
