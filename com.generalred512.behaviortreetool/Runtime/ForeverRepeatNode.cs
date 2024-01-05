namespace GeneralRed512.BehaviorTreeTool
{
    public class ForeverRepeatNode : DecoratorNode
    {
        protected override void OnStart(Tick tick)
        {
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            child.NodeUpdate(tick);
            return State.Running;
        }
    }
}