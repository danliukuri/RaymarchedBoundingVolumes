using RBV.Infrastructure;

namespace RBV.FourDimensional.Infrastructure
{
    public class Raymarching4DServicesRegister : RaymarchingServicesRegister
    {
        protected override void RegisterObjectTypeCaster(IServiceContainer container)
        {
            // container.RegisterAsSingle<IObjectTypeCaster>(new ObjectTypeCaster4D());
        }

        protected override void RegisterTransformTypeCaster(IServiceContainer container)
        {
            // container.RegisterAsSingle<ITransformTypeCaster>(new TransformTypeCaster4D());
        }
    }
}