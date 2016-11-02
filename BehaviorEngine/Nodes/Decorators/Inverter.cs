
//namespace BehaviorEngine
//{
//    public class Inverter : ANodeDecorator
//    {
//        public override void Update()
//        {
//            if(Child == null)
//            {
//                Status = NodeState.Error;
//                return;
//            }

//            Child.Update();
//            Status = Child.Status;

//            if (Status == NodeState.Successful) Status = NodeState.Failure;
//            else if (Status == NodeState.Failure) Status = NodeState.Successful;
//        }
//    }
//}