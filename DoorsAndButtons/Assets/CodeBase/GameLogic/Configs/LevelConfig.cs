using System.Collections.Generic;
using CodeBase.GameLogic.Interfaces;
using Unity.Mathematics;

namespace CodeBase.GameLogic.Configs
{
    public class LevelConfig
    {
        public class DoorConfig
        {
            public float3 OpenPosition;
            public float3 ClosedPosition;
            public float MovingSpeed;
            public string ButtonId;
            public ISceneObjectView View; // can be null
        }

        public class ButtonConfig
        {
            public string ID;
            public float3 Position;
            public float Radius;
            public ISceneObjectView View; // can be null
        }

        public class Actor
        {
            public float3 Position;
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
}
