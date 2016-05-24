
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

        public override NodeState Update()
        {
            var status = Child.Update();
            if (status != NodeState.Active) status = SuccessConditionDelegate();
            return status;
            
        }
    }
}
