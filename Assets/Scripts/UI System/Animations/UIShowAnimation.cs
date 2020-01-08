using System.Collections;
using UnityEngine;

namespace Game.UISystem
{
    [System.Serializable]
    abstract class UIShowAnimation : ScriptableObject
    {
        [SerializeField] float _animationTime;
        [SerializeField] AnimationCurve _animationCurve;
        [SerializeField] [HideInInspector] internal bool isAnimationRunning;

        internal virtual void Animate(BaseUI baseUI)
        {
            baseUI.GetComponent<Canvas>().enabled = true;
            baseUI.StopAnimationCoroutine();
        }

        protected virtual IEnumerator Animation(BaseUI baseUI)
        {
            float elapsed = 0;
            float perc;
            OnAnimationStart(baseUI);
            while (elapsed < _animationTime)
            {
                perc = elapsed / _animationTime;
                OnAnimationRunning(baseUI, _animationCurve.Evaluate(perc));
                elapsed += Time.deltaTime;
                yield return 0f;
            }
            OnAnimationEnd(baseUI);
            yield return 0f;
        }

        protected virtual void OnAnimationStart(BaseUI baseUI)
        {
            isAnimationRunning = true;
        }

        protected abstract void OnAnimationRunning(BaseUI baseUI, float perc);

        internal virtual void OnAnimationEnd(BaseUI baseUI)
        {
            baseUI.StopAnimationCoroutine();
            isAnimationRunning = false;
        }
    }
}
