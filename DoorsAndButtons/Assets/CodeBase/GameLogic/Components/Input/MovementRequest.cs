using Unity.Mathematics;

namespace CodeBase.GameLogic.Components.Input
{
    // Translate to ecs world information about position where user want to send controled actor
    public struct MovementRequest
    {
        public float3 Value;
    }
}
