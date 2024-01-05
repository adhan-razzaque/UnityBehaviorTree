namespace GeneralRed512.BehaviorTreeTool
{
    public class BlackboardTrueNode : ActionNode
    {
        public string key;

        protected override void OnStart(Tick tick)
        {
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            var isTrue = tick.Blackboard.Get<bool>(key).Value;

            return isTrue ? State.Success : State.Failure;
        }
    }
}