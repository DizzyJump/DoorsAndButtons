using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneSettingsView : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    public float Radius;
}
