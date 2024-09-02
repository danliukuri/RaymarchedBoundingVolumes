using RBV.Features.ShaderDataForming;
using RBV.FourDimensional.Features.ShaderDataForming;
using RBV.Infrastructure;

namespace RBV.FourDimensional.Infrastructure
{
    public class Raymarching4DServicesRegister : RaymarchingServicesRegister
    {
        protected override void RegisterObjectTypeCaster(IServiceContainer container) =>
            container.RegisterAsSingle<IObjectTypeCaster>(new Object4DTypeCaster());

        protected override void RegisterTransformTypeCaster(IServiceContainer container) =>
            container.RegisterAsSingle<ITransformTypeCaster>(new Transform4DTypeCaster());

        protected override void RegisterRaymarchedObjectShaderPropertyIdProvider(IServiceContainer container) =>
            container.RegisterAsSingle<IRaymarchedObjectShaderPropertyIdProvider>(
                new RaymarchedObject4DShaderPropertyIdProvider()
            );
    }
}