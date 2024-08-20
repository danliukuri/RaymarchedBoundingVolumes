namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingFeatureEventsSubscriber
    {
        IRaymarchingFeatureEventsSubscriber SubscribeToFeatureEvents();
        IRaymarchingFeatureEventsSubscriber UnsubscribeFromFeatureEvents();
    }
}