using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public interface ISceneObjectView
{
    void UpdatePosition(float3 position);
    void DestroyView();
}
