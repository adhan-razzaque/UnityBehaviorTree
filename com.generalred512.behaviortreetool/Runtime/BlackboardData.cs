using System;

namespace GeneralRed512.BehaviorTreeTool
{
    public abstract class BlackBoardData
    {
        public abstract BlackBoardData GetCopy();
    }

    public class BlackboardData<T> : BlackBoardData
    {
        private T _data;

        public T Value
        {
            get => _data;
            set => _data = value;
        }
        
        public BlackboardData() { }

        public BlackboardData(T value)
        {
            _data = value;
        }

        public Type GetTypeofValue()
        {
            return _data.GetType();
        }

        void Reset()
        {
            _data = default;
        }

        public override BlackBoardData GetCopy()
        {
            BlackboardData<T> copy = new BlackboardData<T>(_data);
            return copy;
        }
    }
}