using System;
using UnityEngine;

namespace RBV.Utilities.Wrappers
{
    public static class ObservableExtensions
    {
        public static ObservableTransform<Vector3> SetValuesFrom(this ObservableTransform<Vector3> observableTransform,
                                                                 Transform                         transform)
        {
            observableTransform.Position.Value = transform.position;
            observableTransform.Rotation.Value = transform.rotation.eulerAngles;
            observableTransform.Scale.Value    = transform.localScale;

            return observableTransform;
        }

        public static ObservableValue<TTo> Cast<TFrom, TTo>(this ObservableValue<TFrom> source,
                                                            Func<TFrom, TTo>            convertor)
        {
            var newObservableValue = new ObservableValue<TTo>(convertor.Invoke(source.Value));
            source.Changed += AssignSourceValueToNewObservableValue;
            return newObservableValue;

            void AssignSourceValueToNewObservableValue(ChangedValue<TFrom> changedValue)
            {
                if (newObservableValue != default)
                    newObservableValue.Value = convertor.Invoke(changedValue.New);
                else
                    source.Changed -= AssignSourceValueToNewObservableValue;
            }
        }
    }
}