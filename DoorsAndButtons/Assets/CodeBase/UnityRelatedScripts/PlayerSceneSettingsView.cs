using CodeBase.GameLogic.Interfaces;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts
{
    public class PlayerSceneSettingsView : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        public float MovementSpeed;

        public bool isListenInput;

        public ISceneObjectView View => gameObject.GetComponent<SceneObjectView>();
    }
}
