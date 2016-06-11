
using System.Collections;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        public List<INode> ChildrenSuccessDeterminers { get; set; }
        public List<INode> ChildrenFailureDeterminers { get; set; }

        public bool SucceedIfOneChildSucceeds { get; set; }
        public bool FailIfOneChildFails { get; set; }

        private int index = 0;


        public Parallel()
        {
            ChildrenSuccessDeterminers = new List<INode>();
            ChildrenFailureDeterminers = new List<INode>();
        }

        public override void Update()
        {
            if(Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            for (index=0; index < Children.Count; index++)
            {
                Children[index].Update();
                Status = Children[index].Status;

                if (Status == NodeState.Failure)
                {
                    if (ChildrenFailureDeterminers.Contains(Children[index]))
                    {
                        return;
                    }

                    Children[index].End();
                    Children[index].Start();
                }
                else if(Status == NodeState.Successful)
                {
                    if (ChildrenSuccessDeterminers.Contains(Children[index]))
                    {
                        return;
                    }

                    Children[index].End();
                    Children[index].Start();
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
            foreach (var c in Children)
            {
                if(c.Status == NodeState.Active) c.End();
            }
        }
    }
}
