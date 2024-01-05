using System;
using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class SequencerNode : CompositeNode
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
            var child = children[_currentChild];

            switch (child.NodeUpdate(tick))
            {
                case State.Running:
                    return State.Running;
                case State.Success:
                    ++_currentChild;
                    break;
                case State.Failure:
                    return State.Failure;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _currentChild == children.Count ? State.Success : State.Running;
        }
    }
}