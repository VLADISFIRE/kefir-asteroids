using System;

namespace UI
{
    public class StartWindow : IDisposable
    {
        private StartWindowLayout _layout;

        public StartWindow(StartWindowLayout layout)
        {
            _layout = layout;

            _layout.button.onClick.AddListener(HandleClick);
        }

        public void Dispose()
        {
            _layout.button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            _layout.gameObject.SetActive(false);
        }
    }
}