using System.Collections.Generic;

namespace GeneralRed512.BehaviorTreeTool
{
    public class Blackboard
    {
        protected Dictionary<string, BlackBoardData> Data = new Dictionary<string, BlackBoardData>();

        public BlackboardData<T> Get<T>(string key)
        {
            if (Data.TryGetValue(key, out BlackBoardData rawResult))
            {
                return rawResult as BlackboardData<T>;
            }
            
            var result = new BlackboardData<T>();
            Data[key] = result;
            return result;
        }

        public bool Remove(string key)
        {
            return Data.Remove(key);
        }

        public void Set<T>(string key, T value)
        {
            Data[key] = new BlackboardData<T>(value);
        }
    }
}