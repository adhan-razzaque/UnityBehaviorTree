using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class RootNode : Node
    {
        [HideInInspector] public Node child;

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        protected override void OnStart(Tick tick)
        {
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            return child.NodeUpdate(tick);
        }
    }
}
