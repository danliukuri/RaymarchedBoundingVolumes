using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Infrastructure
{
    public interface IServiceContainer
    {
        public static IServiceContainer Global { get; protected set; }

        public static IServiceContainer Scoped(Scene scene) => Global.GetScopeContainer(scene);
        protected     IServiceContainer GetScopeContainer(Scene scene);

        TService RegisterAsSingle<TService>(TService implementation);
        TService Resolve<TService>();
    }
}