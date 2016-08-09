
using BehaviorEngine.Utilities;

namespace BehaviorEngine
{
    public class Selector : ANodeComposite
    {
        private int index = 0,
                    indexLastActive = -1;

        private int[] IndexOrder = new int[] { };

        private SequenceBuilder sequenceBuilder = new SequenceBuilder();


        public override void Update()
        {
            if(Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            for (index = 0; index < IndexOrder.Length; index++)
            {
                if (IndexOrder[index] != indexLastActive)
                {
                    Children[index].Start();
                }

                Children[IndexOrder[index]].Update();
                Status = Children[IndexOrder[index]].Status;

                if (Status == NodeState.Successful) break;
                else if (Status == NodeState.Active)
                {
                    if (IndexOrder[index] != indexLastActive)
                    {
                        if (indexLastActive >= 0)
                        {
                            Children[indexLastActive].End();
                        }
                        indexLastActive = IndexOrder[index];
                    }
                    break;
                }
            }
        }

        public override void Start()
        {
            base.Start();
            indexLastActive = -1;
            DetermineOrder();
        }

        public override void End()
        {
            base.End();
            indexLastActive = -1;
        }

        private void DetermineOrder()
        {
            IndexOrder = sequenceBuilder.GetSequence(0, Children.Count-1);
            //IndexOrder = sequenceBuilder.GetRandomSequence(0, Children.Count);
        }
    }
}
