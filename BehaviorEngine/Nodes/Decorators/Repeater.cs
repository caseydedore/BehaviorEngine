

namespace BehaviorEngine
{
    public class Repeater : ANodeDecorator
    {
        private uint RepeatTimesMax { get; set; }
        private uint currentRepeatCount = 0;

        private bool ignoreChildStatus = false;

        private NodeState Status { get; set; }


        public Repeater(uint timesToRepeat, bool ignoreChildStatus = false)
        {
            RepeatTimesMax = timesToRepeat;
            this.ignoreChildStatus = ignoreChildStatus;
        }

        public override NodeState Update()
        {
            if (Child == null) return NodeState.Error;

            if(Status != NodeState.Active)
            {
                currentRepeatCount--;
                Child.Start();
            }

            Status = Child.Update();

            if (Status != NodeState.Active) Child.End();

            if (ignoreChildStatus && currentRepeatCount <= 0) return NodeState.Successful;

            return Status;
        }

        public override void Start()
        {
            currentRepeatCount = RepeatTimesMax;
            Status = NodeState.Error;
        }

        public override void End()
        {
            Child.End();
        }
    }
}