using Unity.Mathematics;

// Translate to ecs world information about position where user want to send controled actor
namespace CodeBase.GameLogic.Components
{
    public struct MovementRequest
    {
        public float3 Value;
    }
}
