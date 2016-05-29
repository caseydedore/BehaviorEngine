

namespace BehaviorEngine
{
    public class Repeater : ANodeDecorator
    {
        private uint RepeatTimesMax { get; set; }
        private uint currentRepeatCount = 0;

        private bool ignoreChildStatus = false;


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

            if(Status != NodeState.Active)
            {
                currentRepeatCount--;
                Child.Start();
            }

            Child.Update();
            Status = Child.Status;

            if (Status != NodeState.Active)
            {
                Child.End();

                if (ignoreChildStatus) Status = NodeState.Successful;
            }
            else if (ignoreChildStatus && currentRepeatCount <= 0) Status = NodeState.Successful;
        }

        public override void Start()
        {
            currentRepeatCount = RepeatTimesMax;
            Status = NodeState.Inactive;
        }

        public override void End()
        {
            Child.End();
            Status = NodeState.Inactive;
        }
    }
}