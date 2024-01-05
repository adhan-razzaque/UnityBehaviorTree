using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        public BehaviorTree behaviorTree;
        public Tick Tick;

        private void Start()
        {
            Tick = new Tick(new Blackboard(), this);
            behaviorTree = behaviorTree.Clone();
        }

        private void Update()
        {
            behaviorTree.TreeUpdate(Tick);
        }
    }
}