using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    GameLogicEngine engine;

    private void Start()
    {
        // Just for testing purposes we will build level settings from unity scene.
        // In real case level settings could be stored on server side or mocked for tests
        LevelConfig levelConfig = BuildLevelConfig();

        engine = new GameLogicEngine();
        engine.Init(levelConfig);

    }

    private void Update()
    {
        CheckUserInput();
        engine?.Update(Time.deltaTime);
    }

    void CheckUserInput()
    {
        if(engine != null && Input.GetMouseButtonUp(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance = 0;
            if(plane.Raycast(ray, out distance))
            {
                var targetPoint = ray.GetPoint(distance);
                targetPoint.y = 0;
                engine.SetInput(targetPoint);
            }

        }
    }

    private void OnDestroy()
    {
        engine?.Shootdown();
    }

    LevelConfig BuildLevelConfig()
    {
        var doorsInScene = GameObject.FindObjectsOfType<DoorSceneSettingsView>();
        var buttonsInScene = GameObject.FindObjectsOfType<ButtonSceneSettingsView>();
        var players = GameObject.FindObjectsOfType<PlayerSceneSettingsView>();

        LevelConfig config = new LevelConfig();

        // fill doors configs data
        foreach(var door in doorsInScene)
        {
            LevelConfig.DoorConfig workDoor = new LevelConfig.DoorConfig();
            workDoor.OpenPosition = door.OpenPosition;
            workDoor.ClosedPosition = door.ClosedPosition;
            workDoor.MovingSpeed = door.MovingSpeed;
            workDoor.ButtonId = door.ButtonToOpen.ID;
            workDoor.View = door.View;

            config.Doors.Add(workDoor);
        }

        // fill buttons configs data
        foreach(var button in buttonsInScene)
        {
            LevelConfig.ButtonConfig workButton = new LevelConfig.ButtonConfig();
            workButton.Position = button.Position;
            workButton.Radius = button.Radius;
            workButton.View = button.View;

            config.Buttons.Add(workButton);
        }

        // fill actors config data. There are only player so far
        foreach(var player in players)
        {
            LevelConfig.Actor workActor = new LevelConfig.Actor();
            workActor.Position = player.Position;
            workActor.MovementSpeed = player.MovementSpeed;
            workActor.ListenInput = player.isListenInput;
            workActor.View = player.View;

            config.Actors.Add(workActor);
        }

        return config;
    }
}
