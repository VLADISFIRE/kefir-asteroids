using System;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Game/Settings", fileName = "GameSettings")]
    public class GameSettingsScrobject : ScriptableObject
    {
        public PlayerSettings player;
        public SpawnerSettings spawner;
    }

    [Serializable]
    public class PlayerSettings
    {
        public Sprite sprite;
        public RocketSettings rocket;
    }

    [Serializable]
    public class RocketSettings
    {
        public float enginePower = 500;
        public float rotateSpeed = 2;
    }

    [Serializable]
    public class SpawnerSettings
    {
        public float delay = 5;
        public Sprite asteroid;
    }
}