using System.Collections.Generic;

namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        private int index = 0;

        private NodeState[] ChildrenStatusAlias { get; set; }


        public override void Update()
        {
            if (Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            for (index = 0; index < Children.Count; index++)
            {
                if (ChildrenStatusAlias[index] != NodeState.Active) continue;

                Children[index].Update();
                Status = Children[index].Status;
                ChildrenStatusAlias[index] = Status;

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

        public override void Start()
        {
            base.Start();

            if (ChildrenStatusAlias == null)
            {
                ChildrenStatusAlias = new NodeState[Children.Count];
            }

            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].Start();
                ChildrenStatusAlias[i] = NodeState.Active;
            }
        }
    }
}
