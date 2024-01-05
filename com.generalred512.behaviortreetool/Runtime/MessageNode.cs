using UnityEngine;

namespace GeneralRed512.BehaviorTreeTool
{
    public class MessageNode : ActionNode
    {
        public string message;
        public string key;

        private GameObject _gameObject;

        protected override void OnStart(Tick tick)
        {
            _gameObject = tick.Blackboard.Get<GameObject>(key).Value;

            if (!_gameObject)
            {
                Debug.LogWarning($"MessageNode did not find gameObject with key {key}");
                return;
            }

            Debug.Log($"MessageNode found gameObject with key {key}");
        }

        protected override void OnStop(Tick tick)
        {
        }

        protected override State OnUpdate(Tick tick)
        {
            if (!_gameObject) return State.Failure;

            _gameObject.SendMessage(message, SendMessageOptions.RequireReceiver);
            return State.Success;
        }
    }
}