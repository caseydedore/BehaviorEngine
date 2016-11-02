
//namespace BehaviorEngine
//{ 

//    public class RepeaterInfinite : ANodeDecorator
//    {
//        private bool ignoreChildStatus = false;

//        private NodeState childStatus = NodeState.Inactive;


//        public RepeaterInfinite(bool ignoreChildStatus = false)
//        {
//            this.ignoreChildStatus = ignoreChildStatus;
//        }

//        public override void Update()
//        {
//            if (Child == null)
//            {
//                Status = NodeState.Error;
//                return;
//            }

//            if (childStatus != NodeState.Active)
//            {
//                Child.Start();
//            }

//            Child.Update();
//            childStatus = Child.Status;
//            Status = childStatus;

//            if (Status != NodeState.Active)
//            {
//                Child.End();

//                if (ignoreChildStatus) Status = NodeState.Active;
//            }
//        }

//        public override void StartRoutine()
//        {
//            base.Start();
//            childStatus = NodeState.Inactive;
//        }

//        public override void EndNode()
//        {
//            base.EndRoutine();
//            childStatus = NodeState.Inactive;
//        }
//    }
//}
