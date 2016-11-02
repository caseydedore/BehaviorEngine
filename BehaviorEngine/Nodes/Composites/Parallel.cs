
using System;
using System.Linq;

namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        public bool ShouldUpdateUntilAllChildrenComplete { get; set; }
        public bool ShouldSuccessBreakTieWhenAllChildrenComplete { get; set; }

        private NodeState Status { get; set; }
        private NodeState[] ChildrenStatusAlias { get; set; }

        private int index = 0;


        public Parallel()
        {
            ShouldUpdateUntilAllChildrenComplete = false;
        }

        protected override NodeState UpdateRoutine()
        {
           

            Status = GetFinalStatus();

            return GetFinalStatus();
        }

        protected override void StartRoutine()
        {
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

        protected int GetNumberOfChildrenWithStatus(NodeState state)
        {
            var childrenWithState = ChildrenStatusAlias.Select(x => x).Where(x => x == state);

            return childrenWithState.Count();
        }

        protected override void ChildFinished(NodeState state)
        {
            //a child should cause this node to re-evaluate in the engine
            throw new NotImplementedException();
        }
    }
}
