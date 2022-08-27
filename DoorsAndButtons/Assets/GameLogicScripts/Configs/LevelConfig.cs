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
        public ISceneObjectView View; // can be null
    }

    public class ButtonConfig
    {
        public int ID;
        public Vector3 Position;
        public float Radius;
        public ISceneObjectView View; // can be null
    }

    public class Actor
    {
        public Vector3 Position;
        public float MovementSpeed;
        public bool ListenInput;
        public ISceneObjectView View; // can be null
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
