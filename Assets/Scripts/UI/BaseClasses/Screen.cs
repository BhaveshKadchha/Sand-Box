using UnityEngine;

namespace Game.UI
{
    public sealed class Screen : BaseUI
    {
        public Screen(Canvas canvas, RectTransform rect, ShowAnimation showAnimation, HideAnimation hideAnimation)
        {
            this.canvas = canvas;

            if (rect == null)
                this.rect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
            else
                this.rect = rect;

            this.showAnimation = showAnimation;
            this.hideAnimation = hideAnimation;
        }

        public override void Show(MonoBehaviour mono)
        {
            base.Show(mono);
        }

        public override void Hide(MonoBehaviour mono, System.Action action)
        {
            base.Hide(mono, action);
        }
    }
}