using Unity.VisualScripting;
using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class InverterNode : DecoratorNode
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

            switch (nodeState)
            {
                case State.Success:
                    return State.Failure;
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Success;
                default:
                    Debug.LogError("Hit an invalid state in Inverter Update");
                    return State.Failure;
            }
        }
    }
}