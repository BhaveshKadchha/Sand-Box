using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public abstract class PopUp : BaseUI
    {
        public override void Show(MonoBehaviour mono)
        {
            base.Show(mono);
        }

        public virtual void Hide(MonoBehaviour mono)
        {
            base.Hide(mono);
        }
    }

    public sealed class SimplePopUp : PopUp
    {
        public SimplePopUp(Canvas canvas, RectTransform rect, ShowAnimation showAnimation, HideAnimation hideAnimation)
        {
            this.canvas = canvas;

            if (rect == null)
                this.rect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
            else
                this.rect = rect;

            this.showAnimation = showAnimation;
            this.hideAnimation = hideAnimation;
        }

        public SimplePopUp(Canvas canvas, RectTransform rect, ShowAnimation showAnimation, HideAnimation hideAnimation, Button closeButton, MonoBehaviour mono)
        {
            this.canvas = canvas;

            if (rect == null)
                this.rect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
            else
                this.rect = rect;

            this.showAnimation = showAnimation;
            this.hideAnimation = hideAnimation;

            closeButton.onClick.AddListener(() => Hide(mono));
        }


        public override void Show(MonoBehaviour mono)
        {
            base.Show(mono);
        }

        public override void Hide(MonoBehaviour mono)
        {
            base.Hide(mono);
        }
    }

    public sealed class GlobalMessagePopUp : PopUp
    {
        public Text text;

        public GlobalMessagePopUp(Canvas canvas, RectTransform rect, ShowAnimation showAnimation, HideAnimation hideAnimation, Button closeButton, MonoBehaviour mono, Text text)
        {
            this.canvas = canvas;

            if (rect == null)
                this.rect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
            else
                this.rect = rect;

            this.showAnimation = showAnimation;
            this.hideAnimation = hideAnimation;
            this.text = text;
            closeButton.onClick.AddListener(() => Hide(mono));
        }

        public void Show(MonoBehaviour mono, string message)
        {
            text.text = message;
            base.Show(mono);
        }

        public override void Hide(MonoBehaviour mono)
        {
            base.Hide(mono);
        }
    }

    public sealed class GlobalWarningPopUp : PopUp
    {
        public Text text;
        public Button acceptButton;

        public GlobalWarningPopUp(Canvas canvas, RectTransform rect, ShowAnimation showAnimation, HideAnimation hideAnimation, Button closeButton, MonoBehaviour mono, Text text, Button acceptButton)
        {
            this.canvas = canvas;

            if (rect == null)
                this.rect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
            else
                this.rect = rect;

            this.showAnimation = showAnimation;
            this.hideAnimation = hideAnimation;
            this.text = text;
            this.acceptButton = acceptButton;

            closeButton.onClick.AddListener(() => Hide(mono));
        }

        public void Show(MonoBehaviour mono, string message, System.Action action, bool hidePopup)
        {
            text.text = message;
            acceptButton.onClick.RemoveAllListeners();
            acceptButton.onClick.AddListener(() =>
            {
                action();
                if (hidePopup)
                    Hide(mono);
            });

            Show(mono);
        }

        public override void Hide(MonoBehaviour mono)
        {
            base.Hide(mono);
        }
    }
}