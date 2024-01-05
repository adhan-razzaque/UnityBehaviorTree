using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class RepeatUntilNode : DecoratorNode
    {
        public string loopCondition;

        private bool _isRunning;
        
        protected override void OnStart(Tick tick)
        {
            tick.Blackboard.Set(loopCondition, false);
            _isRunning = false;
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            _isRunning = tick.Blackboard.Get<bool>(loopCondition).Value;
            if (_isRunning)
            {
                return State.Success;
            }
            
            child.NodeUpdate(tick);
            return State.Running;
        }
    }
}