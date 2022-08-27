using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObjectView
{
    void UpdatePosition(Vector3 position);
    void DestroyView();
}
