using System.Threading;
using System.Threading.Tasks;
using CodeBase.GameLogic;
using CodeBase.GameLogic.Configs;
using CodeBase.UnityRelatedScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private readonly IGameStateMachine gameStateMachine;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingCurtain loadingCurtain;
        private readonly IGameplayModeService gameplayModeService;

        public LoadLevelState(IGameStateMachine gameStateMachine, 
            ISceneLoader sceneLoader, 
            ILoadingCurtain loadingCurtain, 
            IGameplayModeService gameplayModeService)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingCurtain = loadingCurtain;
            this.gameplayModeService = gameplayModeService;
        }

        public void Enter(string sceneName)
        {
            Debug.Log($"LoadLevelState enter. Load scene {sceneName}");
            loadingCurtain.Show();
            sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            // Just for testing purposes we will build level settings from unity scene.
            // In real case level settings could be stored on server side or mocked for tests
            LevelConfig levelConfig = BuildLevelConfig();
            
            gameplayModeService.InitializeGameplaySession(levelConfig);
            
            gameplayModeService.Tick(); // run ecs systems once to prewarm ecs world
            
            gameStateMachine.Enter<GameLoopState>();
        }

        LevelConfig BuildLevelConfig()
        {
            LevelConfig config = new LevelConfig();

            FillDoorsConfigData(config);
            FillButtonsConfigData(config);
            FillActorsConfigData(config);
            FillButtonTriggerConfigData(config);

            return config;
        }

        private void FillButtonTriggerConfigData(LevelConfig config)
        {
            var buttonTriggersInScene = GameObject.FindObjectsOfType<ButtonTriggerSceneSettingsView>();

            foreach (var trigger in buttonTriggersInScene)
            {
                LevelConfig.ButtonTriggerConfig workTriggerConfig = new LevelConfig.ButtonTriggerConfig();
                
                workTriggerConfig.ButtonID = trigger.TriggerButton.ID;
                workTriggerConfig.ActionType = trigger.Action;
                
                config.ButtonTriggers.Add(workTriggerConfig);
            }
        }

        private static void FillActorsConfigData(LevelConfig config)
        {
            var actorsSpawnPointsInScene = GameObject.FindObjectsOfType<PlayerSceneSettingsView>();
            
            foreach (var player in actorsSpawnPointsInScene)
            {
                LevelConfig.ActorConfig workActorConfig = new LevelConfig.ActorConfig();
                
                workActorConfig.Position = player.Position;
                workActorConfig.MovementSpeed = player.MovementSpeed;
                workActorConfig.ListenInput = player.isListenInput;

                config.Actors.Add(workActorConfig);
            }
        }

        private static void FillButtonsConfigData(LevelConfig config)
        {
            var buttonsInScene = GameObject.FindObjectsOfType<ButtonSceneSettingsView>();
            
            foreach (var button in buttonsInScene)
            {
                LevelConfig.ButtonConfig workButton = new LevelConfig.ButtonConfig();
                
                workButton.ID = button.ID;
                workButton.Position = button.Position;
                workButton.Radius = button.Radius;
                workButton.View = button.View;

                config.Buttons.Add(workButton);
            }
        }

        private static void FillDoorsConfigData(LevelConfig config)
        {
            var doorsInScene = GameObject.FindObjectsOfType<DoorSceneSettingsView>();
            
            foreach (var door in doorsInScene)
            {
                LevelConfig.DoorConfig workDoor = new LevelConfig.DoorConfig();

                workDoor.ID = door.ID;
                workDoor.OpenPosition = door.OpenPosition;
                workDoor.ClosedPosition = door.ClosedPosition;
                workDoor.MovingSpeed = door.MovingSpeed;
                workDoor.ButtonId = door.ButtonToOpen.ID;
                workDoor.View = door.View;

                config.Doors.Add(workDoor);
            }
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
        {
        }
    }
}