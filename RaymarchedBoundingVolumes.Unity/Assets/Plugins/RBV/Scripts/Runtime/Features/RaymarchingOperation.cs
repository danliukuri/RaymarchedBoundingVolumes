using System;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.HierarchicalStates;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Dynamic.ShaderData.OperationType;
using RBV.Data.Static.Enumerations;
using RBV.Infrastructure;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.Features
{
    public class RaymarchingOperation : RaymarchingHierarchicalFeature<RaymarchingOperation>
    {
        public event Action<RaymarchingOperation> Changed;
        public event Action<RaymarchingOperation> TypeChanged;
        public event Action<RaymarchingOperation> TypeDataChanged;

        [field: SerializeField] public ObservableValue<RaymarchingOperationType> Type     { get; private set; }
        [field: SerializeField] public ObservableOperationTypeShaderData         TypeData { get; private set; }

        private IRaymarchingChildrenCalculator _raymarchingChildrenCalculator;

        public OperationChildrenData Children { get; private set; }

        public RaymarchingOperationShaderData ShaderData => new()
        {
            OperationType = (int)Type.Value,
            TypeDataIndex = TypeDataIndex
        };

        public override IRaymarchingFeatureHierarchicalState HierarchicalState =>
            new RaymarchingOperationHierarchicalState
                { BaseState = base.HierarchicalState, Type = Type.Value, Children = Children };

        public IOperationTypeShaderData TypeShaderData => TypeData.GetShaderData(Type.Value);

        public int TypeDataIndex { get; set; }

        protected override void Construct()
        {
            base.Construct();
            Construct(IServiceContainer.Global.Resolve<IRaymarchingChildrenCalculator>());
        }

        public void Construct(IRaymarchingChildrenCalculator raymarchingChildrenCalculator) =>
            _raymarchingChildrenCalculator = raymarchingChildrenCalculator;

        public OperationChildrenData CalculateChildrenCount() =>
            Children = _raymarchingChildrenCalculator.CalculateChildrenCount(this);

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            Type.Changed     += RaiseChangedEvent;
            Type.Changed     += RaiseTypeChangedEvent;
            TypeData.Changed += RaiseTypeDataChangedEvent;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            Type.Changed     -= RaiseChangedEvent;
            Type.Changed     -= RaiseTypeChangedEvent;
            TypeData.Changed -= RaiseTypeDataChangedEvent;
        }

        private void RaiseChangedEvent(ChangedValue<RaymarchingOperationType>     type) => Changed?.Invoke(this);
        private void RaiseTypeChangedEvent(ChangedValue<RaymarchingOperationType> type) => TypeChanged?.Invoke(this);

        private void RaiseTypeDataChangedEvent(ChangedValue<IOperationTypeShaderData> data) =>
            TypeDataChanged?.Invoke(this);
    }
}