
namespace BehaviorEngine
{
    public class Parallel : ANodeComposite
    {
        public bool ChildrenMustAllSucceed { get; set; }
        public bool ChildrenMustAllFail { get; set; }

        private int index = 0;

        private int childrenSucceeded = 0,
                    childrenFailed = 0;

        public Parallel()
        {
            ChildrenMustAllSucceed = false;
            ChildrenMustAllFail = false;
        }


        public override void Update()
        {
            if (Children.Count <= 0)
            {
                Status = NodeState.Error;
                return;
            }

            for (index = 0; index < Children.Count; index++)
            {
                if (Children[index].Status != NodeState.Active)
                {
                    Children[index].Start();
                }

                Children[index].Update();
                Status = Children[index].Status;

                if (Status == NodeState.Failure)
                {
                    Children[index].End();
                    childrenFailed++;

                    if (ChildrenMustAllFail && childrenFailed < Children.Count) Status = NodeState.Active;

                    return;
                }
                else if (Status == NodeState.Successful)
                {
                    Children[index].End();

                    if(ChildrenMustAllSucceed && childrenSucceeded < Children.Count) Status = NodeState.Active;

                    return;
                }
            }
        }
    }
}
