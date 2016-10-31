
namespace BehaviorEngine
{
    public class Repeater : ANodeDecorator
    {
        private uint RepeatTimesMax { get; set; }
        private uint currentRepeatCount = 0;

        private bool ignoreChildStatus = false;

        private NodeState childStatus = NodeState.Inactive;


        public Repeater(uint timesToRepeat, bool ignoreChildStatus = false)
        {
            RepeatTimesMax = timesToRepeat;
            this.ignoreChildStatus = ignoreChildStatus;
        }

        public override void Update()
        {
            if (Child == null)
            {
                Status = NodeState.Error;
                return;
            }

            if(currentRepeatCount <= 0)
            {
                Status = NodeState.Inactive;
                return;
            }

            if(childStatus != NodeState.Active)
            {
                currentRepeatCount--;
                Child.Start();
            }

            Child.Update();
            childStatus = Child.Status;
            Status = childStatus;

            if (Status != NodeState.Active)
            {
                Child.End();
                DetermineStatusIfIgnoringChildStatus();
            }
        }

        public override void StartRoutine()
        {
            base.Start();
            currentRepeatCount = RepeatTimesMax;
            childStatus = NodeState.Inactive;
        }

        public override void EndNode()
        {
            base.EndRoutine();
            childStatus = NodeState.Inactive;
        }

        private void DetermineStatusIfIgnoringChildStatus()
        {
            if (ignoreChildStatus && currentRepeatCount <= 0) Status = NodeState.Successful;
            else if (ignoreChildStatus) Status = NodeState.Active;
        }
    }
}