using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SceneObjectView : MonoBehaviour, ISceneObjectView
{
    public void DestroyView()
    {
        Destroy(gameObject);
    }

    public void UpdatePosition(float3 position)
    {
        transform.position = position;
    }
}
