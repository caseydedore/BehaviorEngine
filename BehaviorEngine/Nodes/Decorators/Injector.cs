
using System.Collections;
using System;

namespace BehaviorEngine
{
    public class Injector : ANodeDecorator
    {
        private Func<NodeState> SuccessConditionDelegate;


        public Injector(Func<NodeState> getSuccessCondition)
        {
            SuccessConditionDelegate += getSuccessCondition;
        }

        public override void Update()
        {
            Child.Update();
            var Status = Child.Status;

            if (Status != NodeState.Active) Status = SuccessConditionDelegate();            
        }
    }
}
