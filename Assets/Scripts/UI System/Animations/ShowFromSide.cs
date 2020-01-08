using System.Collections;
using UnityEngine;

namespace Game.UISystem
{
    [CreateAssetMenu(menuName = "UI Animations/Show/Move")]
    class ShowFromSide : UIShowAnimation
    {
        RectTransform _rect;
        [SerializeField] bool _isFromLeft;
        int _targetPoint;

        private void OnEnable()
        {
            if (_isFromLeft)
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
            float xPos = Helper.Map(0, 1, _targetPoint, 0, perc);
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
