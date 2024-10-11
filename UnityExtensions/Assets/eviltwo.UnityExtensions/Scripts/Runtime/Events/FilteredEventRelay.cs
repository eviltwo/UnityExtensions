using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace eviltwo.UnityExtensions
{
    public class FilteredEventRelay : MonoBehaviour
    {
        public enum ValueType
        {
            Value = 0,
            ElapsedFrame = 1,
            ElapsedTime = 2,
        }

        public enum ComparisonOperator
        {
            Equal = 0,
            NotEqual = 1,
            Greater = 2,
            GreaterOrEqual = 3,
            Less = 4,
            LessOrEqual = 5,
        }

        [System.Serializable]
        public class ComparisonExpression
        {
            public ValueType LeftValue = ValueType.Value;
            public ComparisonOperator Comparison = ComparisonOperator.Equal;
            public float RightValue = 0;
        }

        [SerializeField]
        public List<ComparisonExpression> Filters = new List<ComparisonExpression>() { new ComparisonExpression() };

        [SerializeField]
        public UnityEvent<float> OnCallFiltered = new UnityEvent<float>();

        private int _elapsedFrames;
        private float _elapsedTime;

        private void OnEnable()
        {
            _elapsedFrames = 0;
            _elapsedTime = 0;
        }

        private void Update()
        {
            _elapsedFrames++;
            _elapsedTime += Time.unscaledDeltaTime;
        }

        public void Call()
        {
            CallInternal(0);
        }

        public void Call(float value)
        {
            CallInternal(value);
        }

        public void Call(int value)
        {
            CallInternal(value);
        }

        public void Call(bool value)
        {
            CallInternal(value ? 1 : 0);
        }

        private void CallInternal(float value)
        {
            var passed = true;
            var filterCount = Filters.Count;
            for (int i = 0; i < filterCount; i++)
            {
                var filter = Filters[i];
                var leftValue = GetValue(filter.LeftValue, value);
                if (!Compare(leftValue, filter.Comparison, filter.RightValue))
                {
                    passed = false;
                    break;
                }
            }
            if (passed)
            {
                OnCallFiltered.Invoke(value);
            }
        }

        private float GetValue(ValueType valueType, float sampleValue)
        {
            switch (valueType)
            {
                case ValueType.Value:
                    return sampleValue;
                case ValueType.ElapsedFrame:
                    return _elapsedFrames;
                case ValueType.ElapsedTime:
                    return _elapsedTime;
                default:
                    Debug.LogError("Unknown value type: " + valueType);
                    return -1;
            }
        }

        private bool Compare(float leftValue, ComparisonOperator comparison, float rightValue)
        {
            switch (comparison)
            {
                case ComparisonOperator.Equal:
                    return leftValue == rightValue;
                case ComparisonOperator.NotEqual:
                    return leftValue != rightValue;
                case ComparisonOperator.Greater:
                    return leftValue > rightValue;
                case ComparisonOperator.GreaterOrEqual:
                    return leftValue >= rightValue;
                case ComparisonOperator.Less:
                    return leftValue < rightValue;
                case ComparisonOperator.LessOrEqual:
                    return leftValue <= rightValue;
                default:
                    Debug.LogError("Unknown comparison operator: " + comparison);
                    return false;
            }
        }
    }
}
