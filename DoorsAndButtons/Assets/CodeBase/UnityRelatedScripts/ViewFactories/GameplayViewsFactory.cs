using CodeBase.GameLogic.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.UnityRelatedScripts.ViewFactories
{
    public class GameplayViewsFactory : IGameplayViewsFactory
    {
        private SceneObjectView.Factory viewFactory;
        
        public GameplayViewsFactory(SceneObjectView.Factory viewFactory) => 
            this.viewFactory = viewFactory;

        public ISceneObjectView CreatePlayer(float3 position)
        {
            var view = viewFactory.Create(PrefabPaths.Player);
            view.transform.position = position;
            SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
            return view;
        }
    }
}