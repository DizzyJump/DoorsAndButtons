using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    private void Start()
    {
        // Just for testing purposes we will build level settings from unity scene.
        // In real case level settings could be stored on server side or mocked for tests
        LevelConfig levelConfig = BuildLevelConfig();
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

            config.Doors.Add(workDoor);
        }

        // fill buttons configs data
        foreach(var button in buttonsInScene)
        {
            LevelConfig.ButtonConfig workButton = new LevelConfig.ButtonConfig();
            workButton.Position = button.Position;
            workButton.Radius = button.Radius;
        }

        // fill actors config data. There are only player so far
        foreach(var player in players)
        {
            LevelConfig.Actor workActor = new LevelConfig.Actor();
            workActor.Position = player.Position;
            workActor.Rotation = player.Rotation;
        }

        return config;
    }
}
