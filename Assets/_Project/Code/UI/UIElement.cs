using UnityEngine;

namespace UI
{
    public class UIElement
    {
        private GameObject _root;

        private bool _active;

        public bool active { get { return _active; } }

        public UIElement(GameObject root)
        {
            _root = root;
        }

        public void Show()
        {
            if (_active) return;

            SetActive(true);

            OnShow();
        }

        protected virtual void OnShow()
        {
        }

        public void Hide()
        {
            if (!_active) return;

            SetActive(false);

            OnHide();
        }

        protected virtual void OnHide()
        {
        }

        private void SetActive(bool value)
        {
            _root.SetActive(value);
            _active = value;
        }
    }
}