
using System;

namespace BehaviorEngine
{
    public class SelectorRandom : ANodeComposite
    {
        private int index = 0;
        private int IndexMax { get; set; }
        private Random Random { get; set; }


        public override void Update()
        {
            if (Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            if(index == -1)
            {
                index = Random.Next(IndexMax);
            }

            Children[index].Update();
            Status = Children[index].Status;
        }

        public override void Start()
        {
            base.Start();
            index = -1;
            IndexMax = Children.Count - 1;
        }

        public override void End()
        {
            base.End();
            index = -1;
        }
    }
}
