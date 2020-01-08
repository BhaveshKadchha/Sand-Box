using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "UI Animation/Hide/Scale")]
    public class ScaleHide : HideAnimation
    {
        public override void OnAnimationStart()
        {
            base.OnAnimationStart();
            _rect.localScale = Vector3.one;
        }

        public override void OnAnimationRunning(float perc)
        {
            _rect.localScale = Vector3.one * (1 - perc);
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            _rect.localScale = Vector3.zero;
        }
    }
}