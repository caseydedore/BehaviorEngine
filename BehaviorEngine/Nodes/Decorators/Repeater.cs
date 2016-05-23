

namespace BehaviorEngine
{
    public class Repeater : ANodeDecorator
    {
        public int NumTimes { get; private set; }
        private int numTimes = 0;


        //NumTimes of -1 means infinite
        public Repeater(ABehaviorEngine master, int numTimes = -1) : base(master)
        {
            NumTimes = numTimes;
        }

        public override NodeState Update()
        {
            Child.End();
            Child.Start();
            var state = Child.Update();

            if (numTimes > 0)
            {
                if (NumTimes != -1) numTimes--;
            }
            else
            {
                state =  NodeState.Successful;
            }

            return state;
        }

        public override void Start()
        {
            base.Start();
            if (NumTimes == -1) numTimes = 1;
            else numTimes = NumTimes;
        }
    }
}