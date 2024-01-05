namespace GeneralRed512.BehaviorTreeTool
{
    public class RepeatNode : DecoratorNode
    {
        public int numLoops;
        private int _loopsDone = 0;

        protected override void OnStart(Tick tick)
        {
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            if (_loopsDone < numLoops)
            {
                child.NodeUpdate(tick);
                ++_loopsDone;
                return State.Running;
            }

            return State.Success;
        }
    }
}