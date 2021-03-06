﻿
namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        public bool ShouldUpdateUntilAllChildrenComplete { get; set; }
        public bool ShouldSuccessBreakTieWhenAllChildrenComplete { get; set; }


        private NodeState[] ChildrenStatusAlias { get; set; }

        private int index = 0;


        public Parallel()
        {
            ShouldUpdateUntilAllChildrenComplete = false;
        }

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

                if (Children[index].Status != NodeState.Active) Children[index].Start();

                Children[index].Update();
                Status = Children[index].Status;
                ChildrenStatusAlias[index] = Status;

                if (Status == NodeState.Failure || Status == NodeState.Successful)
                {
                    Children[index].End();
                    if (!ShouldUpdateUntilAllChildrenComplete) break;
                }
                else if (Status == NodeState.Error)
                {
                    Children[index].End();
                    break;
                }
            }

            Status = GetFinalStatus();
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

        private NodeState GetFinalStatus()
        {
            if (Status == NodeState.Error) return Status;
            else if (ShouldUpdateUntilAllChildrenComplete) return DetermineStatusForCompleteReturnPolicy();
            else return DetermineStatusForSingleReturnPolicy();
        }

        private NodeState DetermineStatusForSingleReturnPolicy()
        {
            return Status;
        }

        private NodeState DetermineStatusForCompleteReturnPolicy()
        {
            if (GetNumberOfChildrenWithStatus(NodeState.Active) > 0) return NodeState.Active;

            var numberSucceeded = GetNumberOfChildrenWithStatus(NodeState.Successful);
            var numberFailed = GetNumberOfChildrenWithStatus(NodeState.Failure);

            if (numberSucceeded > numberFailed) return NodeState.Successful;
            else if (numberSucceeded < numberFailed) return NodeState.Failure;

            if (ShouldSuccessBreakTieWhenAllChildrenComplete) return NodeState.Successful;
            else return NodeState.Failure;
        }
    }
}
