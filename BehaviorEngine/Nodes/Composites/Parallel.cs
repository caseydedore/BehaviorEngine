
using System.Collections;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        public List<INode> ChildrenSuccessDeterminers { get; set; }
        public List<INode> ChildrenFailureDeterminers { get; set; }


        public Parallel(ABehaviorEngine master)
        {
            ChildrenSuccessDeterminers = new List<INode>();
            ChildrenFailureDeterminers = new List<INode>();
        }

        public override void Update()
        {
            for (int i=0; i < Children.Count; i++)
            {
                Children[i].Update();
                Status = Children[i].Status;

                if (Status == NodeState.Failure)
                {
                    if (ChildrenFailureDeterminers.Contains(Children[i]))
                    {
                        return;
                    }

                    Children[i].End();
                    Children[i].Start();
                }
                else if(Status == NodeState.Successful)
                {
                    if (ChildrenSuccessDeterminers.Contains(Children[i]))
                    {
                        return;
                    }

                    Children[i].End();
                    Children[i].Start();
                }
            }
        }

        public override void Start()
        {
            base.Start();

            foreach(var c in Children)
            {
                c.Start();
            }
        }

        public override void End()
        {
            base.End();
            foreach (var c in Children) c.End();
        }
    }
}
