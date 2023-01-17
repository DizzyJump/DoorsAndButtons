using Unity.Mathematics;

namespace CodeBase.GameLogic.Interfaces
{
    public interface ISceneObjectView
    {
        void UpdatePosition(float3 position);
        void DestroyView();
    }
}
