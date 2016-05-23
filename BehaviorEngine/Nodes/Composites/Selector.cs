
namespace BehaviorEngine
{
    public class Selector : ANodeComposite
    {
        private int index = 0,
                    indexLastActive = -1;
        private NodeState status = NodeState.Successful;


        public Selector(ABehaviorEngine master) : base(master) { }

        public override NodeState Update()
        {
            for (index = 0; index < Children.Count; index++)
            {
                if (index != indexLastActive)
                {
                    Children[index].Start();
                }

                status = Children[index].Update();

                if (status == NodeState.Successful)
                {
                    End();
                    break;
                }
                else if (status == NodeState.Active)
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

            return status;
            /*
            //all nodes need to be checked
            index = 0;
            while(index < Children.Count)
            {
                if (index != indexLastActive)
                {
                    indexLastActive = index;
                    Children[index].Start();
                }

                status = Children[index].Update();

                if (status == NodeState.Successful)
                {
                    End();
                }
                else if (status != NodeState.Active)
                {
                    index++;
                    //override status so parent recognises this node as active upon return
                    status = NodeState.Active;
                }
                return status;
            }

            End();
            return NodeState.Failure;
            */
        }

        public override void Start()
        {
            base.Start();
            //index = 0;
            indexLastActive = -1;
        }

        public override void End()
        {
            base.End();
            foreach (var c in Children) c.End();
            //index = 0;
            indexLastActive = -1;
        }
    }
}
