using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Game/Settings", fileName = "GameSettings")]
    public class GameSettingsScrobject : ScriptableObject
    {
        public GameSettings settings;
    }
}