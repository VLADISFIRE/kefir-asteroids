using UnityEngine;

namespace UI
{
    public class UIElement
    {
        private GameObject _root;

        public UIElement(GameObject root)
        {
            _root = root;
        }

        public void Show()
        {
            _root.SetActive(true);

            OnShow();
        }

        protected virtual void OnShow()
        {
        }

        public void Hide()
        {
            _root.SetActive(false);
            OnHide();
        }

        protected virtual void OnHide()
        {
        }
    }
}