
namespace BehaviorEngine
{
    public class Overrider : ANodeDecorator
    {
        public NodeState SuccessOverride { get { return successOverride; } set { successOverride = value; } }
        public NodeState ActiveOverride { get { return activeOverride; } set { activeOverride = value; } }
        public NodeState FailureOverride { get { return failureOverride; } set { failureOverride = value; } }

        private NodeState successOverride = NodeState.Successful,
                          failureOverride = NodeState.Failure,
                          activeOverride = NodeState.Active;

        private NodeState childStatus = NodeState.Inactive;


        public override void Update()
        {
            if(Child == null)
            {
                Status = NodeState.Error;
                return;
            }

            if (childStatus != NodeState.Active) Child.Start();

            Child.Update();
            childStatus = Child.Status;
            Status = childStatus;

            if (Status == NodeState.Successful) Status = SuccessOverride;
            else if (Status == NodeState.Failure) Status = FailureOverride;
            else if (Status == NodeState.Active) Status = ActiveOverride;

            if (childStatus != NodeState.Active || Status != NodeState.Active) Child.End();
        }

        public override void Start()
        {
            base.Start();
            childStatus = NodeState.Inactive;
        }
    }
}