using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSceneSettingsView : MonoBehaviour
{
    public Vector3 ClosedPosition => transform.position;
    public Vector3 OpenPosition;
    public float MovingSpeed;
    public ButtonSceneSettingsView ButtonToOpen;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.identity;
        if (ButtonToOpen)
            Gizmos.DrawLine(transform.position, ButtonToOpen.Position);
    }
}
