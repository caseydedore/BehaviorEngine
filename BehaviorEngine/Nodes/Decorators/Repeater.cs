

namespace BehaviorEngine
{
    public class Repeater : ANodeDecorator
    {
        private uint RepeatTimesMax { get; set; }
        private uint currentRepeatCount = 0;

        private bool repeatForever = false;
        private bool repeatIgnoringChildStatus = false;

        private NodeState Status { get; set; }


        public Repeater()
        {
            repeatForever = true;
            repeatIgnoringChildStatus = true;
        }

        public Repeater(uint timesToRepeat, bool ignoreChildStatus)
        {
            RepeatTimesMax = timesToRepeat;
            repeatIgnoringChildStatus = ignoreChildStatus;
        }

        public override NodeState Update()
        {
            if (Child == null) return NodeState.Error;

            if(Status != NodeState.Active)
            {
                if(!repeatForever) currentRepeatCount--;
                Child.Start();
            }

            Status = Child.Update();

            if (Status != NodeState.Active)
            {
                Child.End();
                if (!repeatIgnoringChildStatus) Status = NodeState.Active;

                if (!repeatForever && currentRepeatCount <= 0)
                    Status = NodeState.Successful;
            }

            return Status;
        }

        public override void Start()
        {
            base.Start();
            if (!repeatForever) currentRepeatCount = RepeatTimesMax;
        }
    }
}