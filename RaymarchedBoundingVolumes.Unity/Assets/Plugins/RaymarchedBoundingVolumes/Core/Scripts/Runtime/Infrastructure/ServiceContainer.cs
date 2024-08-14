using System;

namespace RaymarchedBoundingVolumes.Infrastructure
{
    public class ServiceContainer : IServiceContainer
    {
        private static readonly Lazy<IServiceContainer> _instance =
            new(() => IServiceContainer.Global = new ServiceContainer());

        private ServiceContainer() { }

        public static IServiceContainer Global => _instance.Value;

        public TService RegisterAsSingle<TService>(TService implementation)
        {
            if (Implementation<TService>.Instance != null)
                throw new InvalidOperationException($"Service of type {typeof(TService).Name} is already registered");

            return Implementation<TService>.Instance = implementation;
        }

        public TService Resolve<TService>()
        {
            TService instance = Implementation<TService>.Instance;
            if (instance == null)
                throw new InvalidOperationException($"Service of type {typeof(TService).Name} is not registered");
            return instance;
        }

        private static class Implementation<TInterface>
        {
            public static TInterface Instance { get; set; }
        }
    }
}