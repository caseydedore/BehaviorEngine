
namespace BehaviorEngine
{
    public class Extender : ANodeDecorator
    {
        public override void Update()
        {
            Child.Update();
            Status = Child.Status;
        }

        public override void Start()
        {
            base.Start();
            Child.Start();
        }
    }
}
