using System;
using System.Collections.Concurrent;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Infrastructure
{
    public class ServiceContainer : IServiceContainer, IDisposable
    {
        private static readonly Lazy<ServiceContainer> _instance = new(() => new ServiceContainer());

        private static readonly ConcurrentDictionary<Scene, ServiceContainer> _sceneScopeContainers = new();
        private readonly        ConcurrentDictionary<Type, object>            _singleInstances      = new();

        private ServiceContainer(string scopeName) => ScopeName = scopeName;
        private ServiceContainer() => ScopeName = nameof(Global);

        public static ServiceContainer Global => _instance.Value;

        public string ScopeName { get; }

        public void Dispose()
        {
            _singleInstances.Clear();
            if (this == Global)
            {
                foreach (ServiceContainer container in _sceneScopeContainers.Values)
                    container.Dispose();
                _sceneScopeContainers.Clear();
            }
        }

        public TService RegisterAsSingle<TService>(TService implementation) =>
            _singleInstances.TryAdd(typeof(TService), implementation)
                ? implementation
                : throw new InvalidOperationException($"Service of type {typeof(TService).Name} is already registered" +
                                                      $" in the container under {ScopeName} scope");

        public TService Resolve<TService>()
        {
            if (_singleInstances.TryGetValue(typeof(TService), out object instance) && instance is TService service)
                return service;

            if (this != Global)
                return Global.Resolve<TService>();

            throw new InvalidOperationException($"Service of type {typeof(TService).Name} is not registered");
        }

        IServiceContainer IServiceContainer.GetScopeContainer(Scene scene) => GetScopeContainer(scene);

        private static ServiceContainer GetScopeContainer(Scene scene)
        {
            if (!_sceneScopeContainers.TryGetValue(scene, out ServiceContainer container))
                _sceneScopeContainers.TryAdd(scene, container = new ServiceContainer(scene.name));
            return container;
        }

        public static IServiceContainer Initialize() => IServiceContainer.Global = _instance.Value;

        public static ServiceContainer Scoped(Scene scene)
        {
            Initialize();
            return GetScopeContainer(scene);
        }
    }
}