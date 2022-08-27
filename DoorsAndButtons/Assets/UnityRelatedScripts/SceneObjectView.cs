using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectView : MonoBehaviour, ISceneObjectView
{
    public void DestroyView()
    {
        Destroy(gameObject);
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }
}
