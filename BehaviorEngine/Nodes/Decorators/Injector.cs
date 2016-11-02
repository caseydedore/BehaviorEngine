//using System;

//namespace BehaviorEngine
//{
//    public class Injector : ANodeDecorator
//    {
//        public Func<NodeState> SuccessConditionDelegate { get; set; }


//        public Injector(Func<NodeState> getSuccessCondition)
//        {
//            SuccessConditionDelegate += getSuccessCondition;
//        }

//        public override void Update()
//        {
//            if(Child == null)
//            {
//                Status = NodeState.Error;
//                return;
//            }

//            Child.Update();
//            Status = Child.Status;

//            if (Status != NodeState.Active && Status != NodeState.Error) Status = SuccessConditionDelegate();            
//        }
//    }
//}
