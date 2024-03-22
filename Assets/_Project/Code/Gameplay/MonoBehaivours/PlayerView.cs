using UnityEngine;

namespace Gameplay.Render
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public void SetEnable(bool value)
        {
            _spriteRenderer.enabled = value;
            
            
        }
    }
}