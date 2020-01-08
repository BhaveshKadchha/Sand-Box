using System.Collections;
using UnityEngine;

namespace Game.UISystem
{
    [CreateAssetMenu(menuName = "UI Animations/Hide/Scale")]
    class ScaleDown : UIHideAnimation
    {
        RectTransform _rect;
        Vector3 _currentScale;

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


        protected override void OnAnimationStart(BaseUI baseUI)
        {
            base.OnAnimationStart(baseUI);
            _rect = baseUI.content.GetComponent<RectTransform>();
            _currentScale = _rect.localScale;
        }

        protected override void OnAnimationRunning(BaseUI baseUI, float perc)
        {
            _rect.localScale = _currentScale * (1 - perc);
            _currentScale = _rect.localScale;
        }

        internal override void OnAnimationEnd(BaseUI baseUI)
        {
            _rect.localScale = Vector3.zero;
            base.OnAnimationEnd(baseUI);
        }

    }
}