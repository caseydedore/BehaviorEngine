
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

        public override NodeState Update()
        {
            for (int i=0; i < Children.Count; i++)
            {
               var status = Children[i].Update();

                if (status == NodeState.Failure)
                {
                    if (ChildrenFailureDeterminers.Contains(Children[i]))
                    {
                        return status;
                    }

                    Children[i].End();
                    Children[i].Start();
                }
                else if(status == NodeState.Successful)
                {
                    if (ChildrenSuccessDeterminers.Contains(Children[i]))
                    {
                        return status;
                    }

                    Children[i].End();
                    Children[i].Start();
                }
            }

            return NodeState.Active;
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
