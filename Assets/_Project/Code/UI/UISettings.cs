using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [CreateAssetMenu(menuName = "Game/UI/Settings", fileName = "UISettings")]
    public class UISettingsScrobject : ScriptableObject
    {
        [FormerlySerializedAs("windows")]
        public Prefabs prefabs;
        
    }

    [Serializable]
    public class Prefabs
    {
        public GameoverWindowLayout gameOver;
        public HudLayout hud;
    }
}