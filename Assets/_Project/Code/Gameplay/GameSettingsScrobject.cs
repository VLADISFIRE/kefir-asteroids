using System;
using Gameplay;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Settings", fileName = "GameSettings")]
    public class GameSettingsScrobject : ScriptableObject
    {
        public PlayerSettings player;
    }

    [Serializable]
    public class PlayerSettings
    {
        public SpeedComponent speed;
    }
}