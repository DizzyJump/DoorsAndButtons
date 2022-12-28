using CodeBase.GameLogic.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts
{
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
}
