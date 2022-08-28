using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

// Translate to ecs world information about position where user want to send controled actor
public struct MovementRequest
{
    public float3 Value;
}
