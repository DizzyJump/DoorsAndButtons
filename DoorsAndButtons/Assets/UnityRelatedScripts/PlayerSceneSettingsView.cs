using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneSettingsView : MonoBehaviour
{
    public Vector3 Position => transform.position;

    public float MovementSpeed;

    public float Radius;

    public bool isListenInput;

    public ISceneObjectView View => gameObject.GetComponent<SceneObjectView>();
}
