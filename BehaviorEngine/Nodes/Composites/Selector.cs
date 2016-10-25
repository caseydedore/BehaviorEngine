
using BehaviorEngine.Utilities;

namespace BehaviorEngine
{
    public class Selector : ANodeComposite
    {
        private const int INDEX_DEFAULT = 0;

        private int index = INDEX_DEFAULT,
                    indexLastActive = INDEX_DEFAULT;

        private int[] IndexOrder = new int[] { };

        protected SequenceBuilder sequenceBuilder = new SequenceBuilder();


        public override void Update()
        {
            for (index = indexLastActive; index < IndexOrder.Length; index++)
            {
                if (index != indexLastActive)
                {
                    Children[IndexOrder[index]].Start();
                    indexLastActive = index;
                }

                Children[IndexOrder[index]].Update();
                Status = Children[IndexOrder[index]].Status;

                if (Status == NodeState.Active) break;
                else
                {
                    Children[IndexOrder[index]].End();

                    if (Status == NodeState.Successful) break;
                }

            }
        }

        public override void Start()
        {
            base.Start();
            indexLastActive = INDEX_DEFAULT;
            IndexOrder = GetIndexOrder();
        }

        public override void End()
        {
            base.End();
            indexLastActive = INDEX_DEFAULT;
        }

        protected virtual int[] GetIndexOrder()
        {
            return sequenceBuilder.GetSequence(0, Children.Count-1);
        }
    }
}
