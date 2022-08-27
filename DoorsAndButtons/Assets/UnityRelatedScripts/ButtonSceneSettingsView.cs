using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneSettingsView : MonoBehaviour
{
    public int ID;
    public Vector3 Position => transform.position;
    public float Radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Position, Radius);
    }
}
