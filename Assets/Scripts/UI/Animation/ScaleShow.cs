using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "UI Animation/Show/Scale")]
    public class ScaleShow : ShowAnimation
    {
        public override void OnAnimationStart()
        {
            base.OnAnimationStart();
            _rect.localScale = Vector3.zero;
        }

        public override void OnAnimationRunning(float perc)
        {
            _rect.localScale = Vector3.one * perc;
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();
            _rect.localScale = Vector3.one;
        }
    }
}