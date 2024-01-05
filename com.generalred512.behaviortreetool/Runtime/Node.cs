using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Running,
            Success,
            Failure
        }

        [HideInInspector] public State state = State.Running;
        [HideInInspector] public bool started;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        public State NodeUpdate(Tick tick)
        {
            if (!started)
            {
                OnStart(tick);
                started = true;
            }
            state = OnUpdate(tick);
            // Debug.Log($"{name} {state}");

            if (state is State.Failure or State.Success)
            {
                OnStop(tick);
                started = false;
            }

            return state;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        protected abstract void OnStart(Tick tick);
        protected abstract void OnStop(Tick tick);
        protected abstract State OnUpdate(Tick tick);
    }
}