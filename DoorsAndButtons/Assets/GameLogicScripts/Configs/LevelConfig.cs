using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig
{
    public class DoorConfig
    {
        public Vector3 OpenPosition;
        public Vector3 ClosedPosition;
        public float MovingSpeed;
        public int ButtonId;
    }

    public class ButtonConfig
    {
        public Vector3 Position;
        public float Radius;
    }

    public class Actor
    {
        public Vector3 Position;
        public float Radius;
        public float MovementSpeed;
        public bool ListenInput;
    }

    public List<DoorConfig> Doors;
    public List<ButtonConfig> Buttons;
    public List<Actor> Actors;

    public LevelConfig()
    {
        Doors = new List<DoorConfig>();
        Buttons = new List<ButtonConfig>();
        Actors = new List<Actor>();
    }
}
