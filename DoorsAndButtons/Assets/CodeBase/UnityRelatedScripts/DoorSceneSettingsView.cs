using CodeBase.GameLogic.Interfaces;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts
{
    [RequireComponent(typeof(UniqueId))]
    public class DoorSceneSettingsView : MonoBehaviour
    {
        public string ID => GetComponent<UniqueId>().Id;
        public Vector3 ClosedPosition => transform.position;
        public Vector3 OpenPosition;
        public float MovingSpeed;
        public ButtonSceneSettingsView ButtonToOpen;
        public ISceneObjectView View => gameObject.GetComponent<SceneObjectView>();

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = Matrix4x4.identity;
            if (ButtonToOpen)
                Gizmos.DrawLine(transform.position, ButtonToOpen.Position);
        }
    }
}