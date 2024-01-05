using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class Tick
    {
        public Blackboard Blackboard { get; }
        
        public Object Target { get; }
        
        public Tick(Blackboard blackboard, Object target)
        {
            Blackboard = blackboard;
            Target = target;
        }
    }
}