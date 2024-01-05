namespace GeneralRed512.BehaviorTreeTool
{
    public class SucceederNode : DecoratorNode
    {
        protected override void OnStart(Tick tick)
        {
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            var nodeState = child.NodeUpdate(tick);
            
            return nodeState == State.Running ? State.Running : State.Success;
        }
    }
}