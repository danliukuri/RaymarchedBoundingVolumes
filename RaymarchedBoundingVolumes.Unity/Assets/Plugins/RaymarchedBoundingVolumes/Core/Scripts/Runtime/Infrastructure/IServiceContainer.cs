namespace RaymarchedBoundingVolumes.Infrastructure
{
    public interface IServiceContainer
    {
        public static IServiceContainer Global { get; protected set; }

        TService RegisterAsSingle<TService>(TService implementation);
        TService Resolve<TService>();
    }
}