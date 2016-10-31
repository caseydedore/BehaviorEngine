
namespace BehaviorEngine
{

    public delegate void NodeEvent(ANode node);
    public delegate void NodeStateEvent(NodeState state);


    public abstract class ANode
    {
        public float UpdateTime { get; protected set; }

        private NodeState State { get; set; }

        protected event NodeEvent StartedEvent;
        protected event NodeEvent EndedEvent;
        private event NodeStateEvent FinishedEvent;

        //parent calls Start, End. Engine calls Update until Status != Active, in which it calls StateReturn

        public void Start()
        {
            StartRoutine();
            Started(this);
        }

        public void End()
        {
            EndRoutine();
            Ended(this);
        }

        public void Update()
        {
            State = UpdateRoutine();
        }

        public void AddStartEvent(NodeEvent e)
        {
            StartedEvent += e;
        }

        public void AddEndEvent(NodeEvent e)
        {
            EndedEvent += e;
        }

        public void AddFinishedEvent(NodeStateEvent e)
        {
            FinishedEvent += e;
        }

        public void Started(ANode n)
        {
            StartedEvent(n);
        }

        public void Ended(ANode n)
        {
            EndedEvent(n);
        }

        public void Finished()
        {
            FinishedEvent(State);
        }

        protected abstract NodeState UpdateRoutine();

        protected virtual void StartRoutine() { }

        protected virtual void EndRoutine() { }
    }
}