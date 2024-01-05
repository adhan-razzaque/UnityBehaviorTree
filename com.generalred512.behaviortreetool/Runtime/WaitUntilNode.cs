namespace GeneralRed512.BehaviorTreeTool
{
    public class WaitUntilNode : ActionNode
    {
        public string flagKey;
        
        private bool _flag;
        
        protected override void OnStart(Tick tick)
        {
            tick.Blackboard.Set(flagKey, false);
            _flag = false;
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            _flag = tick.Blackboard.Get<bool>(flagKey).Value;
            return _flag ? State.Success : State.Running;
        }
    }
}