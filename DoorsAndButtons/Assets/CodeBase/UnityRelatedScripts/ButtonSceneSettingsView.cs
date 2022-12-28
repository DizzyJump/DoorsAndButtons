using CodeBase.GameLogic.Interfaces;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts
{
    public class ButtonSceneSettingsView : MonoBehaviour
    {
        public int ID;
        public Vector3 Position => transform.position;
        public float Radius;
        public ISceneObjectView View => gameObject.GetComponent<SceneObjectView>();

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Position, Radius);
        }
    }
}
