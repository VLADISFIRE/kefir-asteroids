using System;

namespace Gameplay
{
    [Serializable]
    public class GameSettings
    {
        public PlayerSettings player;
        public SpawnerSettings spawner;
    }

    [Serializable]
    public partial class PlayerSettings
    {
        public RocketSettings rocket;
    }

    [Serializable]
    public class RocketSettings
    {
        public float enginePower = 500;
        public float rotateSpeed = 2;
    }

    [Serializable]
    public partial class SpawnerSettings
    {
        public float delay = 5;
    }
}