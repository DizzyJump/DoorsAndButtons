using CodeBase.GameLogic.Interfaces;
using Unity.Mathematics;

namespace CodeBase.UnityRelatedScripts.ViewFactories
{
    public interface IGameplayViewsFactory
    {
        ISceneObjectView CreatePlayer(float3 position);
    }
}