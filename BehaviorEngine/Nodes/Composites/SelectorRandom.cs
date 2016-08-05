
using BehaviorEngine.Utilities;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public class SelectorRandom : ANodeComposite
    {
        private int index = 0;
        private int[] IndexOrder = new int[] { };

        private SequenceBuilder sequenceBuilder = new SequenceBuilder();


        public override void Update()
        {
            if (Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            Children[index].Update();
            Status = Children[index].Status;
        }

        public override void Start()
        {
            base.Start();
            DetermineRandomOrder();
            index = IndexOrder[0];
        }

        public override void End()
        {
            base.End();
            index = -1;
        }

        private void DetermineRandomOrder()
        {
            IndexOrder = sequenceBuilder.GetRandomSequence(0, Children.Count);
        }
    }
}
