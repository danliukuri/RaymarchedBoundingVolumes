namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingFeatureEventsSubscriber
    {
        IRaymarchingFeatureEventsSubscriber SubscribeToFeatureEvents();
        IRaymarchingFeatureEventsSubscriber UnsubscribeFromFeatureEvents();
    }
}