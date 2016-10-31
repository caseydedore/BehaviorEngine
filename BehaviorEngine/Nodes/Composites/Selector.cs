
using BehaviorEngine.Utilities;

namespace BehaviorEngine
{
    public class Selector : ANodeComposite
    {
        private const int INDEX_LAST_ACTIVE_RESET = -1;

        private int index = 0,
                    indexCurrent = 0,
                    indexLastActive = INDEX_LAST_ACTIVE_RESET;

        private int[] IndexOrder = new int[] { };

        protected SequenceBuilder sequenceBuilder = new SequenceBuilder();


        public override void Update()
        {
            for (index = 0; index < IndexOrder.Length; index++)
            {
                if (index != indexLastActive)
                {
                    Children[IndexOrder[index]].Start();
                    indexCurrent = index;
                }

                Children[IndexOrder[index]].Update();
                Status = Children[IndexOrder[index]].Status;

                if (Status == NodeState.Failure && index + 1 == IndexOrder.Length)
                    break;
                else if (Status == NodeState.Active || Status == NodeState.Successful)
                {
                    if(indexLastActive >= 0 && indexLastActive != index)
                        Children[IndexOrder[indexLastActive]].End();
                    indexLastActive = index;
                    break;
                }
            }
        }

        public override void StartRoutine()
        {
            base.Start();
            indexCurrent = 0;
            indexLastActive = INDEX_LAST_ACTIVE_RESET;
            IndexOrder = GetIndexOrder();
        }

        public override void EndNode()
        {
            base.EndNode();
            indexLastActive = INDEX_LAST_ACTIVE_RESET;
            indexCurrent = 0;
        }

        protected virtual int[] GetIndexOrder()
        {
            return sequenceBuilder.GetSequence(0, Children.Count-1);
        }
    }
}
