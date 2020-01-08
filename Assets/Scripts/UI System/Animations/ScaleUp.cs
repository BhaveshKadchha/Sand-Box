using System.Collections;
using UnityEngine;

namespace Game.UISystem
{
    [CreateAssetMenu(menuName = "UI Animations/Show/Scale")]
    class ScaleUp : UIShowAnimation
    {
        RectTransform _rect;

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
            _rect.localScale = Vector3.one * perc;
        }

        protected override void OnAnimationStart(BaseUI baseUI)
        {
            base.OnAnimationStart(baseUI);

            _rect = baseUI.content.GetComponent<RectTransform>();
        }

        internal override void OnAnimationEnd(BaseUI baseUI)
        {
            base.OnAnimationEnd(baseUI);
            _rect.localScale = Vector3.one;
        }
    }
}