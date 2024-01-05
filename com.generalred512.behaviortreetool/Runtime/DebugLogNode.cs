using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class DebugLogNode : ActionNode
    {
        public string message;
        protected override void OnStart(Tick tick)
        {
            Debug.Log($"OnStart: {message}");
        }

        protected override void OnStop(Tick tick)
        {
            Debug.Log($"OnStop: {message}");
        }

        protected override State OnUpdate(Tick tick)
        {
            Debug.Log($"OnUpdate: {message}");
            return State.Success;
        }
    }
}