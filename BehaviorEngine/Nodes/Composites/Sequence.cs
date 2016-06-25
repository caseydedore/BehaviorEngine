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

            foreach (var c in Children)
            {
                if(c.Status == NodeState.Active) c.End();
            }

            index = 0;
            indexLastActive = -1;
        }
    }
}
