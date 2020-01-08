using System.Collections;
using UnityEngine;

namespace Game.UISystem
{
    [CreateAssetMenu(menuName = "UI Animations/Hide/Move")]
    class HideToSide : UIHideAnimation
    {
        RectTransform _rect;
        [SerializeField] bool _isToLeft;
        int _targetPoint;

        private void OnEnable()
        {
            if (_isToLeft)
                _targetPoint = -Screen.width;
            else
                _targetPoint = Screen.width;
        }

        internal override void Animate(BaseUI baseUI)
        {
            base.Animate(baseUI);
            baseUI.StartAnimationCoroutine(Animation(baseUI));
        }

        protected override IEnumerator Animation(BaseUI baseUI)
        {
            baseUI.StartAnimationCoroutine(base.Animation(baseUI));
            yield return null;
        }

        protected override void OnAnimationRunning(BaseUI baseUI, float perc)
        {
            float xPos = Helper.Map(0, 1, 0, _targetPoint, perc);
            _rect.anchoredPosition = new Vector2(xPos, _rect.anchoredPosition.y);
        }

        protected override void OnAnimationStart(BaseUI baseUI)
        {
            base.OnAnimationStart(baseUI);
            _rect = baseUI.content.GetComponent<RectTransform>();
        }

        internal override void OnAnimationEnd(BaseUI baseUI)
        {
            base.OnAnimationEnd(baseUI);
            _rect.anchoredPosition = Vector3.zero;
        }
    }
}