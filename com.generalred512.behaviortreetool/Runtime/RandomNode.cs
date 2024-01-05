using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class RandomNode : CompositeNode
    {
        private int _currentIndex;

        protected override void OnStart(Tick tick)
        {
            _currentIndex = Random.Range(0, children.Count);
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            if (children.Count == 0)
            {
                return State.Success;
            }

            return children[_currentIndex].NodeUpdate(tick);
        }
    }
}