
using System.Collections;
using System;
using System.Collections.Generic;

namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        public bool SucceedIfOneChildSucceeds { get; set; }
        public bool FailIfOneChildFails { get; set; }

        private int childrenSuccessful = 0,
                    childrenFailed = 0;

        private int index = 0;


        public Parallel()
        {
            SucceedIfOneChildSucceeds = true;
            FailIfOneChildFails = true;
        }

        public override void Update()
        {
            if(Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            for (index = 0; index < Children.Count; index++)
            {
                if(Children[index].Status != NodeState.Active)
                {
                    Children[index].Start();
                }

                Children[index].Update();
                Status = Children[index].Status;

                if (Status == NodeState.Failure)
                {
                    childrenFailed++;
                    Children[index].End();

                    if (FailIfOneChildFails || childrenFailed >= Children.Count) return;
                }
                else if(Status == NodeState.Successful)
                {
                    childrenSuccessful++;
                    Children[index].End();

                    if (SucceedIfOneChildSucceeds || childrenSuccessful >= Children.Count) return;
                }
            }
        }
    }
}
