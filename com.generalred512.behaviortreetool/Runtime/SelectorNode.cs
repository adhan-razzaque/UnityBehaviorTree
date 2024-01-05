using System;

namespace GeneralRed512.BehaviorTreeTool
{
    public class SelectorNode : CompositeNode
    {
        private int _currentChild;
        
        protected override void OnStart(Tick tick)
        {
            _currentChild = 0;
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            if (_currentChild == children.Count)
            {
                return State.Failure;
            }
            
            var child = children[_currentChild];

            switch (child.NodeUpdate(tick))
            {
                case State.Running:
                    return State.Running;
                case State.Success:
                    return State.Success;
                case State.Failure:
                    ++_currentChild;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _currentChild == children.Count ? State.Failure : State.Running;
        }
    }
}