
namespace BehaviorEngine
{
    public class Selector : ANodeComposite
    {
        private int index = 0,
                    indexLastActive = -1;


        public override void Update()
        {
            if(Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            for (index = 0; index < Children.Count; index++)
            {
                if (index != indexLastActive)
                {
                    Children[index].Start();
                }

                Children[index].Update();
                Status = Children[index].Status;

                if (Status == NodeState.Successful) break;
                else if (Status == NodeState.Active)
                {
                    if (index != indexLastActive)
                    {
                        if (indexLastActive >= 0)
                        {
                            Children[indexLastActive].End();
                        }
                        indexLastActive = index;
                    }
                    break;
                }
            }
        }

        public override void Start()
        {
            base.Start();
            indexLastActive = -1;
        }

        public override void End()
        {
            base.End();
            indexLastActive = -1;
        }
    }
}
