namespace GeneralRed512.BehaviorTreeTool
{
    public class PlayOnceNode : DecoratorNode
    {
        private bool alreadyPlayed;

        protected override void OnStart(Tick tick)
        {
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            if (!alreadyPlayed) {
                var childResult = child.NodeUpdate(tick);
                if(childResult != State.Running)
                {
                    alreadyPlayed = true;
                }
                return childResult;
            }

            return State.Success;
        }
    }
}