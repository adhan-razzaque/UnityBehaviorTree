using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class RepeatForTimeNode : DecoratorNode
    {
        public float durationInSeconds = 1f;
        private float _startTime;

        protected override void OnStart(Tick tick)
        {
            _startTime = Time.time;
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            if (Time.time - _startTime > durationInSeconds)
            {
                return State.Success;
            }
            child.NodeUpdate(tick);
            return State.Running;
        }
    }
}