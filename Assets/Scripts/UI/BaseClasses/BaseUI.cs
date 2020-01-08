using UnityEngine;

namespace Game.UI
{
    public abstract class BaseUI
    {
        public Canvas canvas;
        public RectTransform rect;
        public ShowAnimation showAnimation;
        public HideAnimation hideAnimation;

        public virtual void Show(MonoBehaviour mono)
        {
            if (showAnimation)
                showAnimation.StartAnimation(mono, rect, () => { canvas.enabled = true; });
            else
                canvas.enabled = true;
        }

        public virtual void Hide(MonoBehaviour mono, System.Action action = null)
        {
            if (hideAnimation)
                hideAnimation.StartAnimation(mono, rect, () => { action?.Invoke(); canvas.enabled = false; });
            else
            {
                canvas.enabled = false;
                action?.Invoke();
            }
        }
    }
}